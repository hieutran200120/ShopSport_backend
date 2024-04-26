using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class District : BaseEntity<Guid>
	{
		public string Name { get; set; }
		public Guid Province_id { get; set; }
		public Province Province { get; set; }
		public ICollection<Order> Orders { get; set; }
		public ICollection<Ward> Wards { get; set; }
	}
	public class DistrictEntityConfiguration : IEntityTypeConfiguration<District>
	{
		public void Configure(EntityTypeBuilder<District> builder)
		{
			builder.ToTable(nameof(District), MainDbContext.OrderSchema);
			builder.HasOne(a => a.Province).WithMany(c => c.Districts).HasForeignKey(c => c.Province_id);
		}
	}
}
