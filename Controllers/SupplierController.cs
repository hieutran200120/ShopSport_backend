using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.Supplier.Dto;
using shopsport.Services.Supplier;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierService _supplierService;
		public SupplierController(ISupplierService supplierService)
		{
			_supplierService = supplierService;
		}
		[HttpGet]
		public async Task<IActionResult> GetSupplier()
		{
			var response = await _supplierService.GetSupplier();
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostSupplier([FromForm] SupplierDto request)
		{
			var res = await _supplierService.PostSupplier(request);
			return Ok(res);
		}
	}
}
