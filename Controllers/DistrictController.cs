using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.District.Dto;
using shopsport.Services.District;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistrictController : ControllerBase
	{
		private readonly IDistrictService _districtService;
		public DistrictController(IDistrictService districtService)
		{
			_districtService = districtService;
		}
		[HttpGet]
		public async Task<IActionResult> GetDistrict()
		{
			var response = await _districtService.GetDistrict();
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostDistrict(RequestDistrictDto request)
		{
			var res = await _districtService.PostDistrict(request);
			return Ok(res);
		}
	}
}
