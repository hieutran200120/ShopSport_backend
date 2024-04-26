using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class User:BaseEntity<Guid>
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PhoneNumber { get; set; }
		public string Avatar { get; set; }
		public List<string> Roles { get; set; }
		public ICollection<Rating> Ratings { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
