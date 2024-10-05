using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Exceptions;
using shopsport.Services.ProductCategoryParent.Dto;
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
		public async Task<SupplierDto> DeleteSupplier(Guid Id)
		{
			var supplier = _mainDbContext.Suppliers.FirstOrDefault(x => x.Id == Id);
			_mainDbContext.Suppliers.Remove(supplier);
			await _mainDbContext.SaveChangesAsync();

			return new SupplierDto
			{
				Name = supplier.Name,
				Adress = supplier.Adress,
				Phone = supplier.Phone,
				Email = supplier.Email,
			};
		}
		public async Task<SupplierDto> UpdateSupplier(Guid Id, SupplierDto request)
		{
			var supplier = _mainDbContext.Suppliers.FirstOrDefault(x => x.Id == Id);
			if (supplier == null)
			{
				throw new RestException(System.Net.HttpStatusCode.NotFound, "No article");
			}
			supplier.Name = request.Name;
			supplier.Adress = request.Adress;
			supplier.Phone = request.Phone;
			supplier.Email = request.Email;

			await _mainDbContext.SaveChangesAsync();
			return new SupplierDto
			{
				Name = supplier.Name,
				Adress = supplier.Adress,
				Phone = supplier.Phone,
				Email = supplier.Email
			};
		}
	}
}
