using shopsport.CommonDto;
using shopsport.Services.Rating.Dto;

namespace shopsport.Services.Rating
{
	public interface IRatingService
	{
		Task<PagingResponseDto<RatingDto>> GetRating();
		Task<RatingDto> PostRating(RatingDto request);
	}
}
