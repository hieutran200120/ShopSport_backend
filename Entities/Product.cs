using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class Product:BaseEntity<Guid>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Image { get; set; }
		public decimal InportPrice { get; set; }//them gia nhap
		public decimal Price { get; set; }
		public decimal PromotionPrice { get; set; }
		public int IsStatus { get; set; }
		public int Quantity { get; set; }
		public Guid ProductCategory_id { get; set; }
		public ProductCategory ProductCategory { get; set; }
		public Guid Supplier_id { get; set; }
		public Supplier Supplier { get; set; }
		public ICollection<Rating> Ratings { get; set; }
		public ICollection<OrderDetail> OrderDetails { get; set; }
		public ICollection<Returns> ReturnProducts { get; set; }
	}
	public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable(nameof(Product), MainDbContext.ProductSchema);
			builder.HasOne(a => a.ProductCategory).WithMany(c => c.ProductCategories).HasForeignKey(c => c.ProductCategory_id);
			builder.HasOne(a => a.Supplier).WithMany(c => c.Products).HasForeignKey(a => a.Supplier_id);
		}
	}
}
