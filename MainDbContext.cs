using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using shopsport.Entities;
using shopsport.Entitycommon;
using System.Drawing;

namespace shopsport
{
	public class MainDbContext : DbContext
	{
		public static string ProductSchema = "product";
		public static string OrderSchema = "order";
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Province> Provinces { get; set; }
		public DbSet<District> Districts { get; set; }
		public DbSet<Ward> Wards { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<ProductCategoryParent> ProductCategoriesParent { get; set; }
		public DbSet<Rating>Ratings { get; set; }


		public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
		}
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			AddAuditInfo();
			return await base.SaveChangesAsync(cancellationToken);
		}

		private void AddAuditInfo()
		{
			var entries = ChangeTracker.Entries().Where(e => e.Entity is IAudiInfo && (e.State == EntityState.Added || e.State == EntityState.Modified));
			foreach (var entry in entries)
			{
				((IAudiInfo)entry.Entity).LastUpdatedAt = DateTime.UtcNow;
				if (entry.State == EntityState.Added)
				{
					((IAudiInfo)entry.Entity).CreatedAt = DateTime.UtcNow;
				}
				else
				{
					Entry(((IAudiInfo)entry.Entity)).Property(x => x.CreatedAt).IsModified = false;
				}
			}
		}
	}
}
