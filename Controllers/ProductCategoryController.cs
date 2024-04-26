using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.ProductCategoty;
using shopsport.Services.ProductCategoty.Dto;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductCategoryController : ControllerBase
	{
		private readonly IProductCategoryService _productCategoryService;
		public ProductCategoryController(IProductCategoryService productCategoryService)
		{
			_productCategoryService = productCategoryService;
		}
		[HttpGet]
		public async Task<IActionResult> GetCategory()
		{
			var response = await _productCategoryService.GetCategory();
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostCategory(CategoryDto request)
		{
			var res = await _productCategoryService.PostCategory(request);
			return Ok(res);
		}
	}
}
