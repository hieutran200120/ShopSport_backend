using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Services.District.Dto;
using shopsport.Services.Product.Dto;
using shopsport.Services.ProductCategoryParent.Dto;

namespace shopsport.Services.ProductCategoryParent
{
	public class ProductCategoryParentService:IProductCategoryParentService
	{
		private readonly MainDbContext _mainDbContext;
		public ProductCategoryParentService(MainDbContext mainDbContext)
		{
			_mainDbContext = mainDbContext;

		}
		public async Task<PagingResponseDto<GetProductParentDto>> GetProductCategoryParent()
		{
			var query = _mainDbContext.ProductCategoriesParent
				.Select(x => new GetProductParentDto
				{
					Id = x.Id,
					Name = x.Name,
				});

			var totalCount = await query.CountAsync();

			var items = await query
				/* .Paging(request.PageIndex, request.Limit)*/
				.ToListAsync();

			return new PagingResponseDto<GetProductParentDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<RequestProductCategoryParent> PostProductCategoryParent(RequestProductCategoryParent request)
		{
			var productCategoryParent = new Entities.ProductCategoryParent
			{
				Name = request.Name,
			};
			await _mainDbContext.ProductCategoriesParent.AddAsync(productCategoryParent);
			await _mainDbContext.SaveChangesAsync();
			return new RequestProductCategoryParent
			{
				Name = productCategoryParent.Name,
			};
		}
	}
}
