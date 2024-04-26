using shopsport.CommonDto;
using shopsport.Services.Product.Dto;
using shopsport.Services.ProductCategoryParent.Dto;

namespace shopsport.Services.ProductCategoryParent
{
	public interface IProductCategoryParentService
	{
		Task<PagingResponseDto<GetProductParentDto>> GetProductCategoryParent();
		Task<RequestProductCategoryParent> PostProductCategoryParent(RequestProductCategoryParent request);
	}
}
