using shopsport.Entitycommon;
using shopsport.Services.User.Dto;

namespace shopsport.Services.Rating.Dto
{
	public class RatingDto:IAudiInfo
	{
		public int star { get; init; }
		public string Content { get;init; }
		public Guid Product_id { get; init; }
		public Guid User_id { get; init; }
		public UserDto User { get; init; }
	}
}
