using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.Supplier.Dto;
using shopsport.Services.Supplier;
using shopsport.Services.ProductCategoryParent.Dto;

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
		public async Task<IActionResult> PostSupplier( SupplierDto request)
		{
			var res = await _supplierService.PostSupplier(request);
			return Ok(res);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteSupplier(Guid Id)
		{
			var res = await _supplierService.DeleteSupplier(Id);
			return Ok(res);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateSupplier([FromQuery] Guid Id, SupplierDto request)
		{
			var res = await _supplierService.UpdateSupplier(Id, request);
			return Ok(res);
		}
	}
}
