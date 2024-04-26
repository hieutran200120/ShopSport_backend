using shopsport.Entitycommon;

namespace shopsport.Services.Order.Dto
{
	public class OrderDto:IAudiInfo
	{
		public string Email { get; init; }
		public string PhoneNumber { get; init; }
		public string Address { get; init; }
		public string Note { get; init; }
		public decimal Price { get; init; }
		public int Status { get; init; }
		public int IsPay { get; init; }
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public Guid Province_id { get; init; }
		public Guid District_id { get; init; }
		public Guid Ward_id { get; init; }
		public Guid User_id { get; init; }
	}
}
