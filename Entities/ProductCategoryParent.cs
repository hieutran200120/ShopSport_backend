using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class ProductCategoryParent : BaseEntity<Guid>
	{
		public string Name { get; set; }
		public ICollection<ProductCategory> ProductCategorys { get; set; }
	}
}
