using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class ProductCategory : BaseEntity<Guid>
	{
		public string Name { get; set; }
		public Guid ProductCategoriesParent_id { get; set; }
		public ProductCategoryParent ProductCategoriesParent { get; set; }
		public ICollection<Product> ProductCategories { get; set; }
	}
}
