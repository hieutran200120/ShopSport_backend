using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.Product.Dto;
using shopsport.Services.Product;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _producService;
		public ProductController(IProductService producService)
		{
			_producService = producService;
		}
		[HttpGet]
		public async Task<IActionResult> GetProduct([FromQuery] QueryGlobalProductRequestDto request)
		{
			var response = await _producService.GetProduct(request);
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostProduct([FromForm] ProductDto request)
		{
			var res = await _producService.PostProduct(request);
			return Ok(res);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(Guid Id)
		{
			var res = await _producService.DeleteProduct(Id);
			return Ok(res);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateProduct([FromQuery] Guid Id, [FromForm] ProductDto request)
		{
			var res = await _producService.UpdateProduct(Id, request);
			return Ok(res);
		}
	}
}
