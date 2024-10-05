using shopsport.CommonDto;

namespace shopsport.Services.Rating.Dto
{
	public class QueryGlobalRatingRequestDto:PagingRequestDto
	{
		public Guid Product_id { get; init; }
	}
}
