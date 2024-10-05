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
		public async Task<IActionResult> GetRating([FromQuery] QueryGlobalRatingRequestDto request)
		{
			var response = await _ratingService.GetRating(request);
			return Ok(response);
		}
		[HttpGet("countRating")]
		public async Task<IActionResult>  CountRating([FromQuery] QueryGlobalRatingRequestDto request)
		{
			var response = await _ratingService.CountRating(request);
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> PostRating(RatingDto request)
		{
			var res = await _ratingService.PostRating(request);
			return Ok(res);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteRating(Guid Id)
		{
			var res = await _ratingService.DeleteRating(Id);
			return Ok(res);
		}
	}
}
