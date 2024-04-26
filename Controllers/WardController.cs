using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.ward.Dto;
using shopsport.Services.ward;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WardController : ControllerBase
	{
		private readonly IWardService _WardService;
		public WardController(IWardService WardService)
		{
			_WardService = WardService;
		}
		[HttpGet]
		public async Task<IActionResult> GetWard()
		{
			var response = await _WardService.GetWard();
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostWard(RequestWardDto request)
		{
			var res = await _WardService.PostWard(request);
			return Ok(res);
		}
	}
}
