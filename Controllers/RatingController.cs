using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopsport.Services.Product.Dto;
using shopsport.Services.Product;
using shopsport.Services.Rating;
using shopsport.CommonDto;
using shopsport.Services.Rating.Dto;

namespace shopsport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RatingController : ControllerBase
	{
		private readonly IRatingService _ratingService;
		public RatingController(IRatingService ratingService)
		{
			_ratingService = ratingService;
		}
		[HttpGet]
		public async Task<IActionResult> GetRating()
		{
			var response = await _ratingService.GetRating();
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostRating(RatingDto request)
		{
			var res = await _ratingService.PostRating(request);
			return Ok(res);
		}
	}
}
