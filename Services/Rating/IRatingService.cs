using shopsport.CommonDto;
using shopsport.Services.Rating.Dto;

namespace shopsport.Services.Rating
{
	public interface IRatingService
	{
		Task<PagingResponseDto<GetRatingDto>> GetRating(QueryGlobalRatingRequestDto request);
		Task<RatingDto> PostRating(RatingDto request);
		Task<RatingDto> DeleteRating(Guid Id);
		Task<List<RatingCountDto>> CountRating(QueryGlobalRatingRequestDto request);
	}
}
