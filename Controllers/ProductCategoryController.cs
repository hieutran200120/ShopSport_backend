using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.Product.Dto;
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
		public async Task<IActionResult> GetCategory([FromQuery] QueryGlobalProductCategoryRequestDto request)
		{
			var response = await _productCategoryService.GetCategory(request);
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostCategory(CategoryDto request)
		{
			var res = await _productCategoryService.PostCategory(request);
			return Ok(res);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteCategory(Guid Id)
		{
			var res = await _productCategoryService.DeleteCategory(Id);
			return Ok(res);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateCategory([FromQuery] Guid Id, CategoryDto request)
		{
			var res = await _productCategoryService.UpdateCategory(Id, request);
			return Ok(res);
		}
	}
}
