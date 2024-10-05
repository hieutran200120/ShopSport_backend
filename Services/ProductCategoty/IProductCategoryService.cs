using shopsport.CommonDto;
using shopsport.Services.Product.Dto;
using shopsport.Services.ProductCategoty.Dto;

namespace shopsport.Services.ProductCategoty
{
	public interface IProductCategoryService
	{
		Task<PagingResponseDto<GetCategoryDto>> GetCategory(QueryGlobalProductCategoryRequestDto request);
		Task<CategoryDto> PostCategory(CategoryDto request);
		Task<CategoryDto> DeleteCategory(Guid Id);
		Task<CategoryDto> UpdateCategory(Guid Id, CategoryDto request);
	}
}
