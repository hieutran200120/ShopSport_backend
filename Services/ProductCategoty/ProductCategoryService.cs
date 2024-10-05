using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Exceptions;
using shopsport.LinQ;
using shopsport.Services.Product.Dto;
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
		public async Task<PagingResponseDto<GetCategoryDto>> GetCategory(QueryGlobalProductCategoryRequestDto request)
		{
			var query = _mainDbContext.ProductCategories
				.WhereIf(request.Id != Guid.Empty, x => x.Id.Equals(request.Id))
				.WhereIf(request.CategoryParent_id != Guid.Empty, x => x.ProductCategoriesParent_id.Equals(request.CategoryParent_id))
				.Select(x => new GetCategoryDto
				{
					Id = x.Id,
					Name = x.Name,
				});

			var totalCount = await query.CountAsync();

			var items = await query
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
		public async Task<CategoryDto> DeleteCategory(Guid Id)
		{
			var category = _mainDbContext.ProductCategories.FirstOrDefault(x => x.Id == Id);
			_mainDbContext.ProductCategories.Remove(category);
			await _mainDbContext.SaveChangesAsync();

			return new CategoryDto
			{
				Name = category.Name,

			};
		}
		public async Task<CategoryDto> UpdateCategory(Guid Id, CategoryDto request)
		{
			var category = _mainDbContext.ProductCategories.FirstOrDefault(x => x.Id == Id);
			if (category == null)
			{
				throw new RestException(System.Net.HttpStatusCode.NotFound, "No article");
			}
			category.Name = request.Name;
			category.ProductCategoriesParent_id = request.ProductCategoryParent_id;
			
			await _mainDbContext.SaveChangesAsync();
			return new CategoryDto
			{
				Name = category.Name,
			};
		}
	}
}
