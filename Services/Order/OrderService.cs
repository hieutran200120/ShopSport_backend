using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Entities;
using shopsport.Exceptions;
using shopsport.LinQ;
using shopsport.Services.Order.Dto;
using shopsport.Services.Product.Dto;

namespace shopsport.Services.Order
{
	public class OrderService : IOrderService
	{
		private readonly MainDbContext _mainDbContext;
		public OrderService(MainDbContext mainDbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_mainDbContext = mainDbContext;
		}
		public async Task<PagingResponseDto<OrderRespon>> GetOrder(QueryGlobalOrderRequestDto request)
		{
			var query = _mainDbContext.Orders
				.WhereIf(!string.IsNullOrEmpty(request.FirstName), x => x.FirstName.Contains(request.FirstName))
				.WhereIf(request.User_id != Guid.Empty, x => x.User_id.Equals(request.User_id))
				.WhereIf(request.Order_id!=Guid.Empty,x=>x.Id.Equals(request.Order_id))
				.WhereIf(request.Id != Guid.Empty, x => x.Id.Equals(request.Id))
				.WhereIf(request.Year.HasValue && request.Month.HasValue,
	x => x.CreatedAt.Year == request.Year && x.CreatedAt.Month == request.Month)
				.WhereIf(request.Status.HasValue, x => x.Status == request.Status)
				.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new OrderRespon
		  {
			  Id = x.Id,
			  Order = new OrderDto
			  {
				  FirstName = x.FirstName,
				  LastName = x.LastName,
				  Email = x.Email,
				  PhoneNumber = x.PhoneNumber,
				  Address = x.Address,
				  Note = x.Note,
				  Price = x.Price,
				  Status = x.Status,
				  IsPay = x.IsPay,
				  CreatedAt = x.CreatedAt,
			  },
		  })
		   .ToListAsync();
			var TotalCount = await query.CountAsync();
			return new PagingResponseDto<OrderRespon>
			{
				Items = items,
				TotalCount = TotalCount
			};
		}
		public async Task<PagingResponseDto<OrderRespon>> GetOrderId(QueryGlobalOrderRequestDto request)
		{
			var query = _mainDbContext.OrderDetails
				.WhereIf(request.Order_id != Guid.Empty, x => x.OrderId.Equals(request.Order_id))
				.Include(product => product.Product)
				.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new OrderRespon
		  {
			  Id = x.ProductId,
			  Order = new OrderDto
			  {
				  CreatedAt = x.CreatedAt,
				  Address = x.Order.Address,
				  LastName = x.Order.LastName,
				  FirstName = x.Order.FirstName,
				  Email=x.Order.Email,
				  Status = x.Order.Status
			  },
			  OrderItem = new OrderItemDto
			  {
				  Quantity = x.Quantity,
				  Price = x.Price,
			  },
			  OrderItems = new GetProductDto
			  {
				  Id=x.ProductId,
				  Name = x.Product.Name,
				  Image = x.Product.Image,
			  }
		  })
		   .ToListAsync();
			var TotalCount = await query.CountAsync();
			return new PagingResponseDto<OrderRespon>
			{
				Items = items,
				TotalCount = TotalCount
			};
		}
		public async Task<PagingResponseDto<OrderRespon>> GetOrderReturn(QueryGlobalOrderRequestDto request)
		{
			var query = _mainDbContext.Products
				.WhereIf(request.Order_id != Guid.Empty, x => x.OrderDetails.Equals(request.Order_id))
				.Include(product => product.OrderDetails)
				.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new OrderRespon
		  {
			  Id = x.Id,
			
			  OrderItem = new OrderItemDto
			  {
				  Quantity = x.Quantity,
				  Price = x.Price,
			  },
			  OrderItems = new GetProductDto
			  {
				  Name = x.Name,
				  Image = x.Image,
			  }
		  })
		   .ToListAsync();
			var TotalCount = await query.CountAsync();
			return new PagingResponseDto<OrderRespon>
			{
				Items = items,
				TotalCount = TotalCount
			};
		}
		public async Task<OrderDto> DeleteOrder(Guid Id)
		{
			var order = _mainDbContext.Orders.FirstOrDefault(x => x.Id == Id);
			_mainDbContext.Orders.Remove(order);
			await _mainDbContext.SaveChangesAsync();

			return new OrderDto
			{
				Email=order.Email,
				Status=order.Status,
				Address=order.Address,
				PhoneNumber=order.PhoneNumber,
				Note=order.Note,
				Price=order.Price,

			};
		}
		public async Task<OrderItemDto> AddOrderItems(OrderRequestDto orderRequest)
		{
			var order = new Entities.Order
			{
				FirstName = orderRequest.Order.FirstName,
				LastName = orderRequest.Order.LastName,
				Address = orderRequest.Order.Address,
				Email=orderRequest.Order.Email,
				Note = orderRequest.Order.Note,
				PhoneNumber = orderRequest.Order.PhoneNumber,
				Price = orderRequest.Order.Price,
				Status = orderRequest.Order.Status,
				IsPay = orderRequest.Order.IsPay,
				Province_id = orderRequest.Order.Province_id,
				District_id = orderRequest.Order.District_id,
				Ward_id = orderRequest.Order.Ward_id,
				User_id = orderRequest.Order.User_id
			};

			await _mainDbContext.Orders.AddAsync(order);
			await _mainDbContext.SaveChangesAsync();

			if (orderRequest.OrderItems != null && orderRequest.OrderItems.Any())
			{
				foreach (var item in orderRequest.OrderItems)
				{
					var orderItem = new OrderDetail
					{
						OrderId = order.Id,
						ProductId = item.ProductId,
						Price = item.Price,
						Quantity = item.Quantity
					};
					// Lấy số lượng của sản phẩm trừ đi số lượng của đơn hàng
					var product = await _mainDbContext.Products.FindAsync(item.ProductId);
					if (product != null)
					{
						product.Quantity -= item.Quantity;
					}
					_mainDbContext.OrderDetails.Add(orderItem);
				}

				await _mainDbContext.SaveChangesAsync();
			}

			return new OrderItemDto
			{
				OrderId= order.Id,
			};
		}
		public async Task<changeStatusDto> UpdateOrder(Guid Id, changeStatusDto request)
		{
			var Order = _mainDbContext.Orders.FirstOrDefault(x => x.Id == Id);
			if (Order == null)
			{
				throw new RestException(System.Net.HttpStatusCode.NotFound, "No article");
			}
			Order.Status = request.Status;
			await _mainDbContext.SaveChangesAsync();
			return new changeStatusDto
			{
				Status = request.Status,
			};
		}
		public async Task<ReportOrderProductResult> GetOrderProduct(QueryGlobalOrderRequestDto request)
		{
			var query = _mainDbContext.OrderDetails
				/*.WhereIf(request.Order_id != Guid.Empty, x => x.OrderId.Equals(request.Order_id))*/
				.Include(od => od.Product) // Include Product information
				.OrderByDescending(od => od.CreatedAt);

			var groupedItems = await query
				.GroupBy(od => od.ProductId) 
				.Select(group => new ReportOrderProduct
				{
					Id = group.Key,
					Product = new ProductDto
					{

						Name = group.First().Product.Name,
						Description = group.First().Product.Description,
						Price = group.First().Product.Price,
						InportPrice=group.First().Product.InportPrice,
						PromotionPrice=group.First().Product.PromotionPrice,
						IsStatus=group.First().Product.IsStatus,
						Image=group.First().Product.Image,
						
					},
					TotalQuantity = group.Sum(od => od.Quantity)
				})
				.ToListAsync();

			return new ReportOrderProductResult
			{
				
				ReportOrderProducts = groupedItems
			};
		}
		public async Task<PagingResponseDto<OrderRespon>> ReportRevenue(QueryGlobalOrderRequestDto request)
		{
			var query = _mainDbContext.OrderDetails
				.Include(product => product.Product)
				.Include(od => od.Order)
				.WhereIf(request.Year.HasValue && request.Month.HasValue,
	x => x.CreatedAt.Year == request.Year && x.CreatedAt.Month == request.Month)
				.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new OrderRespon
		  {
			  Id = x.Id,
			  Order = new OrderDto
			  {
				  Price = x.Price,
				  Status = x.Order.Status,
				  CreatedAt = x.CreatedAt,
			  },
			  OrderItem = new OrderItemDto
			  {
				  Quantity = x.Quantity,
				  Price = x.Price,
			  },
			  OrderItems = new GetProductDto
			  {
				  Id = x.ProductId,
				 InportPrice=x.Product.InportPrice,
				 IsStatus=x.Product.IsStatus
			  }
		  })
		   .ToListAsync();
			var TotalCount = await query.CountAsync();
			return new PagingResponseDto<OrderRespon>
			{
				Items = items,
				TotalCount = TotalCount
			};
		}
	}
}
