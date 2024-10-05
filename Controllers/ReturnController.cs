using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.District;
using shopsport.Services.District.Dto;
using shopsport.Services.Returns;
using shopsport.Services.Returns.Dto;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReturnController : ControllerBase
	{
		private readonly IReturnSerive _returnService;
		public ReturnController(IReturnSerive returnService)
		{
			_returnService = returnService;
		}
		[HttpPost]
		public async Task<IActionResult> AddReturnItems(ReturnDto orderRequest)
		{
			var res = await _returnService.AddReturnItems(orderRequest);
			return Ok(res);
		}
		[HttpGet]
		public async Task<IActionResult> GetProduct([FromQuery] QueryGlobalProductReturnRequestDto request)
		{
			var response = await _returnService.GetProduct(request);
			return Ok(response);
		}
	}
}
