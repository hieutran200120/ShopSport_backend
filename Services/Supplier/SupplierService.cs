using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Services.Supplier.Dto;

namespace shopsport.Services.Supplier
{
	public class SupplierService:ISupplierService
	{
		private readonly MainDbContext _mainDbContext;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public SupplierService(MainDbContext mainDbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_mainDbContext = mainDbContext;
			_hostEnvironment = hostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<PagingResponseDto<GetSupplierDto>> GetSupplier()
		{
			var query = _mainDbContext.Suppliers
				.Select(x => new GetSupplierDto
				{
					Id = x.Id,
					Name = x.Name,
					Adress = x.Adress,
					Phone = x.Phone,
					Email = x.Email,				
				});

			var totalCount = await query.CountAsync();

			var items = await query
				.ToListAsync();
			return new PagingResponseDto<GetSupplierDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<SupplierDto> PostSupplier(SupplierDto request)
		{
			var supplier = new Entities.Supplier
			{
				Name = request.Name,
				Adress = request.Adress,
				Phone = request.Phone,
				Email = request.Email,
			};
			await _mainDbContext.Suppliers.AddAsync(supplier);
			await _mainDbContext.SaveChangesAsync();
			return new SupplierDto
			{
				Name = supplier.Name,
				Adress = supplier.Adress,
				Phone = supplier.Phone,
				Email = supplier.Email,
			};
		}
		
	}
}
