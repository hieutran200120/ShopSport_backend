using shopsport.CommonDto;
using shopsport.Services.ProductCategoty.Dto;

namespace shopsport.Services.ProductCategoty
{
	public interface IProductCategoryService
	{
		Task<PagingResponseDto<GetCategoryDto>> GetCategory();
		Task<CategoryDto> PostCategory(CategoryDto request);
	}
}
