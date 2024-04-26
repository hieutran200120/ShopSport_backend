using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class Ward : BaseEntity<Guid>
	{
		public string Name { get; set; }
		public Guid District_id { get; set; }
		public District District { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
	public class WardEntityConfiguration : IEntityTypeConfiguration<Ward>
	{
		public void Configure(EntityTypeBuilder<Ward> builder)
		{
			builder.ToTable(nameof(Ward), MainDbContext.OrderSchema);
			builder.HasOne(a => a.District).WithMany(c => c.Wards).HasForeignKey(c => c.District_id);		
		}
	}
}
