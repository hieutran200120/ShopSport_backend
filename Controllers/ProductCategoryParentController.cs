using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.ProductCategoryParent;
using shopsport.Services.ProductCategoryParent.Dto;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductCategoryParentController : ControllerBase
	{
		private readonly IProductCategoryParentService _productCategoryParentService;
		public ProductCategoryParentController(IProductCategoryParentService productCategoryParentService)
		{
			_productCategoryParentService = productCategoryParentService;
		}
		[HttpGet]
		public async Task<IActionResult> GetProductCategoryParent()
		{
			var response = await _productCategoryParentService.GetProductCategoryParent();
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostProductCategoryParent(RequestProductCategoryParent request)
		{
			var res = await _productCategoryParentService.PostProductCategoryParent(request);
			return Ok(res);
		}
	}
}
