using shopsport.Services.Product.Dto;

namespace shopsport.Services.Order.Dto
{
	public class OrderItemDto
	{
		public decimal Price { get; init; }
		public int Quantity { get; init; }
		public Guid OrderId { get; init; }
		public Guid ProductId { get; init; }
	}
}
