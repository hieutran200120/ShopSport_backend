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
	public class OrderService:IOrderService
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
				.WhereIf(request.Id != Guid.Empty, x => x.Id.Equals(request.Id))
				.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new OrderRespon
		  {
			  Id = x.Id,
			 Order= new OrderDto
			 {	
				 FirstName=x.FirstName,
				 LastName=x.LastName,
				 Email=x.Email,
				 PhoneNumber=x.PhoneNumber,
				 Address=x.Address,
				 Note=x.Note,
				 Price=x.Price,
				 Status=x.Status,
				 IsPay = x.IsPay,
				 CreatedAt=x.CreatedAt,
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
		public async Task<PagingResponseDto<OrderRespon>> GetOrderId(QueryGlobalOrderRequestDto request)
		{
			var query = _mainDbContext.OrderDetails
				.WhereIf(request.Order_id != Guid.Empty, x => x.OrderId.Equals(request.Order_id))
				.Include(product => product.Product)
				.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new OrderRespon
		  {
			  Id = x.Id,
			  Order=new OrderDto
			  {
				  CreatedAt = x.CreatedAt,
				  Address=x.Order.Address,
				  LastName=x.Order.LastName,
				  FirstName=x.Order.FirstName,
				  Status=x.Order.Status
			  },
			  OrderItem=new OrderItemDto
			  {
				  Quantity = x.Quantity,
				  Price = x.Price,
			  },
			 OrderItems=new ProductDto
			 {
				 Name=x.Product.Name,
				 Image=x.Product.Image,
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
		public async Task<OrderRequestDto> AddOrderItems( OrderRequestDto orderRequest)
		{
			var order = new Entities.Order
			{
				FirstName = orderRequest.Order.FirstName,
				LastName = orderRequest.Order.LastName,
				Address = orderRequest.Order.Address,
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

			return new OrderRequestDto
			{
				Order=orderRequest.Order,
				OrderItems=orderRequest.OrderItems
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
				Status= request.Status,
			};
		}
	}
}
