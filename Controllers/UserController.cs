using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.User.Dto;
using shopsport.Services.User;

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
		public async Task<IActionResult> GetCurrentUser()
		{
			var res = await _userService.GetCurrentUser();
			return Ok(res);
		}
		[HttpPost]
		public async Task<IActionResult> Register([FromForm] RegisterDto request)
		{
			var res = await _userService.Register(request);
			return Ok(res);
		}
	}

}
