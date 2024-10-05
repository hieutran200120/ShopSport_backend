using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Entities;
using shopsport.LinQ;
using shopsport.Services.Order.Dto;
using shopsport.Services.Product.Dto;
using shopsport.Services.Returns.Dto;
using shopsport.Services.Supplier.Dto;

namespace shopsport.Services.Returns
{
	public class ReturnService:IReturnSerive
	{
		private readonly MainDbContext _mainDbContext;
		public ReturnService(MainDbContext mainDbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_mainDbContext = mainDbContext;
		}
		public async Task<ReturnDto> AddReturnItems(ReturnDto orderRequest)
		{
			var returnProducts = new List<Entities.Returns>();

			foreach (var item in orderRequest.Returns)
			{
				// Tạo đối tượng returnProduct từ orderRequest
				var returnProduct = new Entities.Returns
				{
					Order_id = item.Order_id,
					Product_id = item.Product_id,
					Reason = item.Reason,
					Quantity = item.Quantity,
					Price = item.Price
				};
				returnProducts.Add(returnProduct);
				// Tìm sản phẩm và cập nhật số lượng
				var product = await _mainDbContext.Products.FirstOrDefaultAsync(p => p.Id == item.Product_id);
				if (product != null)
				{
					product.Quantity += item.Quantity;
					_mainDbContext.Products.Update(product);
				}
				// Tìm đơn hàng và cập nhật trạng thái và giá
				var order = await _mainDbContext.Orders
					.Include(o => o.OrderDetails)
					.FirstOrDefaultAsync(o => o.Id == item.Order_id);
				if (order != null)
				{
					var totalReturnedQuantity = await _mainDbContext.ReturnProducts
						.Where(rp => rp.Order_id == order.Id)
						.SumAsync(rp => rp.Quantity) + item.Quantity;

					// Tính tổng giá trị của tất cả các sản phẩm được trả lại
					var totalReturnedPrice = await _mainDbContext.ReturnProducts
						.Where(rp => rp.Order_id == order.Id)
						.SumAsync(rp => rp.Quantity * rp.Price);

					var totalOrderQuantity = order.OrderDetails.Sum(od => od.Quantity);
					var totalOrderPrice = order.OrderDetails.Sum(od => od.Price);

					// Cập nhật trạng thái của đơn hàng
					order.Status = totalReturnedQuantity >= totalOrderQuantity ? 6 : 5;

					// Tính lại giá trị của đơn hàng sau khi trừ giá trị sản phẩm được trả lại
					var remainingOrderPrice = totalOrderPrice - totalReturnedPrice;
					order.Price = remainingOrderPrice < 0 ? 0 : remainingOrderPrice; // Giá trị không âm
																					 // Cập nhật số lượng của từng sản phẩm trong OrderDetail
					foreach (var orderDetail in order.OrderDetails)
					{
						var returnedQuantity = await _mainDbContext.ReturnProducts
							.Where(rp => rp.Order_id == order.Id && rp.Product_id == orderDetail.ProductId)
							.SumAsync(rp => rp.Quantity);

						orderDetail.Quantity -= returnedQuantity;
						_mainDbContext.OrderDetails.Update(orderDetail);
					}
					_mainDbContext.Orders.Update(order);
				}
			}
			// Lưu các sản phẩm trả lại vào cơ sở dữ liệu
			await _mainDbContext.ReturnProducts.AddRangeAsync(returnProducts);
			await _mainDbContext.SaveChangesAsync();

			return orderRequest;
		}
		public async Task<PagingResponseDto<ReturnItemDto>> GetProduct(QueryGlobalProductReturnRequestDto request)
		{
			var query = _mainDbContext.ReturnProducts
		.Include(product => product.Product)
		.WhereIf(!string.IsNullOrEmpty(request.Name), x => x.Product.Name.ToLower().Contains(request.Name.ToLower()))
		.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new ReturnItemDto
		  {
			  Name=x.Product.Name,
			  Image=x.Product.Image.ToList(),
			  Quantity = x.Quantity,
			 Price = x.Price,
			 Reason = x.Reason,
			 Email=x.Order.Email,
		  }).ToListAsync();
			var TotalCount = await query.CountAsync();
			return new PagingResponseDto<ReturnItemDto>
			{
				Items = items,
				TotalCount = TotalCount
			};

		}
	}
}
