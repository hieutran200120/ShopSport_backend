using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.LinQ;
using shopsport.Services.Rating.Dto;
using shopsport.Services.Supplier.Dto;
using shopsport.Services.User.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace shopsport.Services.Rating
{
	public class RatingService : IRatingService
	{
		private readonly MainDbContext _mainDbContext;
		public RatingService(MainDbContext mainDbContext)
		{
			_mainDbContext = mainDbContext;
		}
		public async Task<PagingResponseDto<GetRatingDto>> GetRating(QueryGlobalRatingRequestDto request)
		{
			var query = _mainDbContext.Ratings
				.Include(rating=>rating.User)
				.Include(product=>product.Products)
				.WhereIf(request.Product_id != Guid.Empty, x => x.Product_id.Equals(request.Product_id))
				.Select(x => new GetRatingDto
				{
					Id=x.Id,
					star=x.star,
					Content=x.Content,
					Product_id=x.Product_id,
					User_id=x.User_id,
					CreatedAt=x.CreatedAt,
					NameProduct=x.Products.Name,
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
				.Paging(request.PageIndex, request.Limit).ToListAsync();
			return new PagingResponseDto<GetRatingDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<List<RatingCountDto>> CountRating(QueryGlobalRatingRequestDto request)
		{
			var ratingCounts = await _mainDbContext.Ratings
				.WhereIf(request.Product_id != Guid.Empty, x => x.Product_id.Equals(request.Product_id))
				.GroupBy(x => x.star)
				.Select(g => new RatingCountDto
				{
					Star = g.Key,
					Count = g.Count()
				})
				 .OrderByDescending(dto => dto.Star)
				.ToListAsync();

			return ratingCounts;
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
		public async Task<RatingDto> DeleteRating(Guid Id)
		{
			var rating = _mainDbContext.Ratings.FirstOrDefault(x => x.Id == Id);
			_mainDbContext.Ratings.Remove(rating);
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
