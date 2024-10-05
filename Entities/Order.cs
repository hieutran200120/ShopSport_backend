using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class Order : BaseEntity<Guid>
	{
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public string Note { get; set; }
		public decimal Price { get; set; }
		public int Status { get; set; }
		public int IsPay { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Guid Province_id { get; set; }
		public Province Province { get; set; }
		public Guid District_id { get; set; }
		public District District { get; set; }
		public Guid Ward_id { get; set; }
		public Ward Ward { get; set; }
		public Guid User_id { get; set; }
		public User User { get; set; }
		public ICollection<OrderDetail> OrderDetails { get; set; }
		public ICollection<Returns> ReturnProducts { get; set; }
	}
	public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable(nameof(Order), MainDbContext.OrderSchema);
			builder.HasOne(a => a.Province).WithMany(c => c.Orders).HasForeignKey(c => c.Province_id);
			builder.HasOne(a=>a.District).WithMany(c=> c.Orders).HasForeignKey(a => a.District_id);
			builder.HasOne(a=>a.Ward).WithMany(c => c.Orders).HasForeignKey(a=>a.Ward_id);
			builder.HasOne(a => a.User).WithMany(c => c.Orders).HasForeignKey(a => a.User_id);
		}
	}
}
