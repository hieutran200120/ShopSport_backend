using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class OrderDetail : BaseEntity<Guid>
	{
			public decimal Price { get; set; }
			public int Quantity { get; set; }
			public Guid OrderId { get; set; }
			public Order Order { get; set; }
			public Guid ProductId { get; set; }	
			public Product Product { get; set; }
		
	}
	public class OrderDetailEntityConfiguration : IEntityTypeConfiguration<OrderDetail>
	{
		public void Configure(EntityTypeBuilder<OrderDetail> builder)
		{
			builder.ToTable(nameof(OrderDetail), MainDbContext.OrderSchema);
			builder.HasOne(a => a.Order).WithMany(c => c.OrderDetails).HasForeignKey(c => c.OrderId);
			builder.HasOne(a => a.Product).WithMany(c => c.OrderDetails).HasForeignKey(a => a.ProductId);	
		}
	}
}
