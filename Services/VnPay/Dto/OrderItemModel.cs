using shopsport.Entities;

namespace shopsport.Services.VnPay.Dto
{
	public class OrderItemModel
	{
		public decimal Price { get; init; }
		public int Quantity { get; init; }
		public Guid OrderId { get; init; }
		public Guid ProductId { get; init; }
	}
}
