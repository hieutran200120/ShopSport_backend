using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class Supplier : BaseEntity<Guid>
	{
		public string Name { get; set; }
		public string Adress { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}
