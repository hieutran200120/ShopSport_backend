using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class Returns : BaseEntity<Guid>
	{
		public Guid Order_id { get; set; }
		public Order Order { get; set; }
		public Guid Product_id { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public string Reason { get; set; }
		public decimal Price { get; set; }
	}
	public class ReturnsEntityConfiguration : IEntityTypeConfiguration<Returns>
	{
		public void Configure(EntityTypeBuilder<Returns> builder)
		{
			builder.ToTable(nameof(Returns), MainDbContext.OrderSchema);
			builder.HasOne(a => a.Order).WithMany(c => c.ReturnProducts).HasForeignKey(c => c.Order_id);
			builder.HasOne(a => a.Product).WithMany(c => c.ReturnProducts).HasForeignKey(a => a.Product_id);
		}
	}
}
