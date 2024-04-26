using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Services.Rating.Dto;
using shopsport.Services.Supplier.Dto;

namespace shopsport.Services.Rating
{
	public class RatingService : IRatingService
	{
		private readonly MainDbContext _mainDbContext;
		public RatingService(MainDbContext mainDbContext)
		{
			_mainDbContext = mainDbContext;
		}
		public async Task<PagingResponseDto<RatingDto>> GetRating()
		{
			var query = _mainDbContext.Ratings
				.Include(rating=>rating.User)
				/*.WhereIf(request.Id != Guid.Empty, x => x.Id.Equals(request.Id))*/
				.Select(x => new RatingDto
				{
					star=x.star,
					Content=x.Content,
					Product_id=x.Product_id,
					User_id=x.User_id,
					CreatedAt=x.CreatedAt,
					User= new User.Dto.UserDto
					{
						LastName=x.User.LastName,
						FirstName=x.User.FirstName,
						Email=x.User.Email,
						PhoneNumber=x.User.PhoneNumber,
						Avatar=x.User.Avatar,
					}
				});

			var totalCount = await query.CountAsync();

			var items = await query
				.ToListAsync();
			return new PagingResponseDto<RatingDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<RatingDto> PostRating(RatingDto request)
		{
			var rating = new Entities.Rating
			{
				star=request.star,
				Content=request.Content,
				Product_id=request.Product_id,
				User_id=request.User_id,
			};
			await _mainDbContext.Ratings.AddAsync(rating);
			await _mainDbContext.SaveChangesAsync();
			return new RatingDto
			{
				star = rating.star,
				Content = rating.Content,
				Product_id = rating.Product_id,
				User_id = rating.User_id
			};
		}
	}
}
