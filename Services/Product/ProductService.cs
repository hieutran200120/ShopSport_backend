using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Exceptions;
using shopsport.LinQ;
using shopsport.Services.Product.Dto;

namespace shopsport.Services.Product
{
	public class ProductService:IProductService
	{
		private readonly MainDbContext _mainDbContext;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ProductService(MainDbContext mainDbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_mainDbContext = mainDbContext;
			_hostEnvironment = hostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<PagingResponseDto<GetProductDto>> GetProduct(QueryGlobalProductRequestDto request)
		{
			var query = _mainDbContext.Products
		.Include(product => product.ProductCategory)
		.WhereIf(request.Id != Guid.Empty, x => x.Id.Equals(request.Id))
		.WhereIf(request.CategoryParent_id != Guid.Empty, x => x.ProductCategory.ProductCategoriesParent_id.Equals(request.CategoryParent_id))
		.WhereIf(request.CategoryList != null && request.CategoryList.Any(), x => request.CategoryList.Contains(x.ProductCategory_id))
		.WhereIf(!string.IsNullOrEmpty(request.Name), x => x.Name.Contains(request.Name))
		.OrderByDescending(x => x.CreatedAt);
			var items = await query
		  .Select(x => new GetProductDto
		  {
			  Id = x.Id,
			  Name = x.Name,
			  Price = x.Price,
			  Quantity = x.Quantity,
			  PromotionPrice = x.PromotionPrice,
			  Description = x.Description,
			  Image = x.Image.ToList(),
			  Category = new Dto.CommonDto { Name = x.ProductCategory.Name, Id = x.ProductCategory.Id },
			  Supplier = new Dto.CommonDto { Name = x.Supplier.Name, Id = x.Supplier.Id }
		  })
		   .ToListAsync();
			var TotalCount = await query.CountAsync();
			return new PagingResponseDto<GetProductDto>
			{
				Items = items,
				TotalCount = TotalCount
			};

		}
		public async Task<GetProductDto> PostProduct(ProductDto request)
		{
			var product = new Entities.Product
			{
				Name = request.Name,
				Price = request.Price,
				PromotionPrice = request.PromotionPrice,
				Quantity= request.Quantity,
				Image = await SaveImages(request.ImageFiles),
				Description = request.Description,
				ProductCategory_id = request.Category.Id,
				Supplier_id = request.Supplier.Id			
			};
			await _mainDbContext.Products.AddAsync(product);
			await _mainDbContext.SaveChangesAsync();
			return new GetProductDto
			{
				Id = product.Id,
				Name = request.Name,
				Description = request.Description,
				Category = new Dto.CommonDto { Id = request.Category.Id },
				Supplier = new Dto.CommonDto { Id = request.Supplier.Id },
				
			};
		}
		public async Task<GetProductDto> DeleteProduct(Guid Id)
		{
			var product = _mainDbContext.Products.FirstOrDefault(x => x.Id == Id);
			_mainDbContext.Products.Remove(product);
			await _mainDbContext.SaveChangesAsync();

			return new GetProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,

			};
		}
		public async Task<GetProductDto> UpdateProduct(Guid Id, ProductDto request)
		{
			var product = _mainDbContext.Products.FirstOrDefault(x => x.Id == Id);
			if (product == null)
			{
				throw new RestException(System.Net.HttpStatusCode.NotFound, "No article");
			}
			product.Name = request?.Name;
			product.Description = request?.Description;
			product.Price = request.Price;
			product.Quantity= request.Quantity;
			product.IsStatus = request.IsStatus;
			product.Image = await SaveImages(request.ImageFiles);
			product.PromotionPrice = request.PromotionPrice;
			product.ProductCategory_id = request.Category.Id;
			product.Supplier_id = request.Supplier.Id;
			await _mainDbContext.SaveChangesAsync();
			return new GetProductDto
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				PromotionPrice = product.PromotionPrice,
				Description = product.Description,
				Image = product.Image,
				Category = new Dto.CommonDto { Name = product.ProductCategory.Name, Id = product.ProductCategory.Id },
				Supplier = new Dto.CommonDto { Name = product.Supplier.Name, Id = product.Supplier.Id }
			};
		}
		public async Task<List<string>> SaveImages(List<IFormFile> imageFiles)
		{
			List<string> imageNames = new List<string>();

			foreach (var imageFile in imageFiles)
			{
				string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
				imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
				var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);

				using (var fileStream = new FileStream(imagePath, FileMode.Create))
				{
					await imageFile.CopyToAsync(fileStream);
				}

				imageNames.Add(imageName);
			}

			return imageNames;
		}
	}
}
