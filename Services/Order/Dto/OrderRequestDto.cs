namespace shopsport.Services.Order.Dto
{
	public class OrderRequestDto
	{
		public OrderDto Order { get; set; }
		public List<OrderItemDto> OrderItems { get; set; }
	}
}
