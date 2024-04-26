using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class Province : BaseEntity<Guid>
	{
		public string Name { get; set; }
		public ICollection<Order> Orders { get; set; }
		public ICollection<District> Districts { get; set; }
	}
}
