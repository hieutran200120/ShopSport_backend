using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.CommonDto;
using shopsport.Services.District;
using shopsport.Services.District.Dto;
using shopsport.Services.Order;
using shopsport.Services.Order.Dto;
using shopsport.Services.Product.Dto;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		[HttpPost]
		public async Task<IActionResult> AddOrderItems	(OrderRequestDto request)
		{
			var res = await _orderService.AddOrderItems(request);
			return Ok(res);
		}
		[HttpGet]
		public async Task<IActionResult> GetOrder([FromQuery] QueryGlobalOrderRequestDto request)
		{
			var response = await _orderService.GetOrder(request);
			return Ok(response);
		}
		[HttpGet("OrderDetail")]
		public async Task<IActionResult> GetOrderId([FromQuery] QueryGlobalOrderRequestDto request)
		{
			var response = await _orderService.GetOrderId(request);
			return Ok(response);
		}
		[HttpGet("OrderProduct")]
		public async Task<IActionResult> GetOrderProduct([FromQuery] QueryGlobalOrderRequestDto request)
		{
			var response = await _orderService.GetOrderProduct(request);
			return Ok(response);
		}
		[HttpGet("ReportRevenue")]
		public async Task<IActionResult> ReportRevenue([FromQuery] QueryGlobalOrderRequestDto request)
		{
			var response = await _orderService.ReportRevenue(request);
			return Ok(response);
		}
		[HttpGet("GetOrderReturn")]
		public async Task<IActionResult> GetOrderReturn([FromQuery] QueryGlobalOrderRequestDto request)
		{
			var response = await _orderService.GetOrderReturn(request);
			return Ok(response);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateOrder([FromQuery] Guid Id, [FromBody] changeStatusDto request)
		{
			var res = await _orderService.UpdateOrder(Id, request);
			return Ok(res);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteOrder(Guid Id)
		{
			var res = await _orderService.DeleteOrder(Id);
			return Ok(res);
		}
	}
}
