using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Services.ProductCategoty.Dto;

namespace shopsport.Services.ProductCategoty
{
	public class ProductCategoryService:IProductCategoryService
	{
		private readonly MainDbContext _mainDbContext;
		public ProductCategoryService(MainDbContext mainDbContext)
		{
			_mainDbContext = mainDbContext;

		}
		public async Task<PagingResponseDto<GetCategoryDto>> GetCategory()
		{
			var query = _mainDbContext.ProductCategories
				.Select(x => new GetCategoryDto
				{
					Id = x.Id,
					Name = x.Name,
				});

			var totalCount = await query.CountAsync();

			var items = await query
				/* .Paging(request.PageIndex, request.Limit)*/
				.ToListAsync();

			return new PagingResponseDto<GetCategoryDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<CategoryDto> PostCategory(CategoryDto request)
		{
			var category = new Entities.ProductCategory
			{
				Name = request.Name,
				ProductCategoriesParent_id=request.ProductCategoryParent_id
			};
			await _mainDbContext.ProductCategories.AddAsync(category);
			await _mainDbContext.SaveChangesAsync();
			return new CategoryDto
			{
				Name = category.Name,
			};
		}
	}
}
