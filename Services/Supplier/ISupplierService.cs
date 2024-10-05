using shopsport.CommonDto;
using shopsport.Services.Supplier.Dto;

namespace shopsport.Services.Supplier
{
	public interface ISupplierService
	{
		Task<PagingResponseDto<GetSupplierDto>> GetSupplier();
		Task<SupplierDto> PostSupplier(SupplierDto request);
		Task<SupplierDto> DeleteSupplier(Guid Id);
		Task<SupplierDto> UpdateSupplier(Guid Id, SupplierDto request);
	}
}
