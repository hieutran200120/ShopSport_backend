using shopsport.CommonDto;
using shopsport.Services.Product.Dto;

namespace shopsport.Services.Product
{
	public interface IProductService
	{
		Task<PagingResponseDto<GetProductDto>> GetProduct(QueryGlobalProductRequestDto request);
		Task<GetProductDto> PostProduct(ProductDto request);
		Task<GetProductDto> DeleteProduct(Guid Id);
		Task<GetProductDto> UpdateProduct(Guid Id, ProductDto request);
	}
}
