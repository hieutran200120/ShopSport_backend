using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.Province.Dto;
using shopsport.Services.Province;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProvinceController : ControllerBase
	{
		private readonly IProvinceService _provinceService;
		public ProvinceController(IProvinceService provinceService)
		{
			_provinceService = provinceService;
		}
		[HttpGet]
		public async Task<IActionResult> GetProvince()
		{
			var response = await _provinceService.GetProvince();
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostProvince(RequestProvinceDto request)
		{
			var res = await _provinceService.PostProvince(request);
			return Ok(res);
		}
	}
}
