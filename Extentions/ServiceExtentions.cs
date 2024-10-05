using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shopsport.Entities;
using shopsport.Infrastructure.Auth;
using shopsport.Services.District;
using shopsport.Services.Order;
using shopsport.Services.Product;
using shopsport.Services.ProductCategoryParent;
using shopsport.Services.ProductCategoty;
using shopsport.Services.Province;
using shopsport.Services.Rating;
using shopsport.Services.Returns;
using shopsport.Services.Supplier;
using shopsport.Services.User;
using shopsport.Services.VnPay;
using shopsport.Services.ward;
using System.Text;

namespace shopsport.Extentions
{
	public static class ServiceExtentions
	{
		public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<MainDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("ShopAPI")).EnableSensitiveDataLogging());
		}
		public static void ConfigDIBusinessService(this IServiceCollection services)
		{
			services.AddScoped<IProvinceService, ProvinceService>();
			services.AddScoped<IDistrictService, DistrictService>();
			services.AddScoped<IWardService, WardService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IProductCategoryService,ProductCategoryService>();
			services.AddScoped<ISupplierService, SupplierService>();
			services.AddScoped<IUserService,UserService>();
			services.AddScoped<IOrderService,OrderService>();
			services.AddScoped<IVnPayService, VnPayService>();
			services.AddScoped<IRatingService, RatingService>();
			services.AddScoped<IProductCategoryParentService, ProductCategoryParentService>();
			services.AddScoped<IReturnSerive,ReturnService>();
		}
		public static void ConfigureMigration(this IServiceCollection services)
		{
			var mainDbContext = services.BuildServiceProvider().GetRequiredService<MainDbContext>();
			if (mainDbContext is not null)
			{
				mainDbContext.Database.Migrate();
			}
		}
		public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ICurrentUser, CurrentUser>();
			var credential = configuration["AppCredential"];
			var key = Encoding.ASCII.GetBytes(credential);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(option =>
			{
				option.RequireHttpsMetadata = false;
				option.SaveToken = true;
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false, //người cấp phát
					ValidateAudience = false,
					ValidateLifetime = true,
					//ClockSkew = TimeSpan.Zero
				};
			});
		}
	}
}
