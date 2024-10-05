using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.Order.Dto;
using shopsport.Services.VnPay;
using shopsport.Services.VnPay.Dto;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PayController : ControllerBase
	{
		private readonly IVnPayService _vnPayService;

		public PayController(IVnPayService vnPayService)
		{
			_vnPayService = vnPayService;
		}

		[HttpPost("create-payment-url")]
		public async Task<IActionResult> CreatePaymentUrl([FromBody] OrderRequestDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var url = await _vnPayService.CreatePaymentUrl(model, HttpContext);

			return Ok(new { redirectUrl = url });
		}
		[HttpGet("payment-callback")]
		public IActionResult PaymentCallback([FromQuery] string queryString)
		{
			var response = _vnPayService.PaymentExecute(Request.Query);

			return Ok(response);
		}
	}
}
