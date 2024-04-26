using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shopsport.Entitycommon;

namespace shopsport.Entities
{
	public class Rating:BaseEntity<Guid>
	{
		public int star { get; set; }
		public string Content { get; set; }
		public Guid Product_id { get; set; }
		public Product Products { get; set; }
		public Guid User_id { get; set; }
		public User User { get; set; }	
	}
	public class RatingEntityConfiguration : IEntityTypeConfiguration<Rating>
	{
		public void Configure(EntityTypeBuilder<Rating> builder)
		{
			builder.ToTable(nameof(Rating), MainDbContext.OrderSchema);
			builder.HasOne(a => a.Products).WithMany(c => c.Ratings).HasForeignKey(c => c.Product_id);
			builder.HasOne(a => a.User).WithMany(c => c.Ratings).HasForeignKey(a => a.User_id);
		}
	}
}
