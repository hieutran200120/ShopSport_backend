using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.User.Dto;
using shopsport.Services.User;
using shopsport.Services.Product.Dto;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequestDto Request)
		{
			var res = await _userService.Login(Request);
			return Ok(res);
		}
		[HttpGet]
		public async Task<IActionResult> GetCurrentUser([FromQuery] QueryGlobalUserRequestDto request)
		{
			var res = await _userService.GetCurrentUser(request);
			return Ok(res);
		}
		[HttpPost]
		public async Task<IActionResult> Register([FromForm] RegisterDto request)
		{
			var res = await _userService.Register(request);
			return Ok(res);
		}
		[HttpDelete]
		public async Task<IActionResult>  DeleteUser(Guid Id)
		{
			var res = await _userService.DeleteUser(Id);
			return Ok(res);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateUser([FromQuery] Guid Id, [FromForm] RegisterDto request)
		{
			var res = await _userService.UpdateUser(Id, request);
			return Ok(res);
		}
	}

}
