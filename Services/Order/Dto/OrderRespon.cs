using shopsport.Entitycommon;
using shopsport.Services.Product.Dto;

namespace shopsport.Services.Order.Dto
{
	public class OrderRespon:IAudiInfo
	{
		public Guid Id { get; set; }
		public OrderDto Order { get; set; }
		public ProductDto OrderItems { get; set; }
		public OrderItemDto OrderItem { get; set; }
	}
}
