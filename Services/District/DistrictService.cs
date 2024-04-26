using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Services.District.Dto;

namespace shopsport.Services.District
{
	public class DistrictService : IDistrictService
	{
		private readonly MainDbContext _mainDbContext;
		public DistrictService(MainDbContext mainDbContext)
		{
			_mainDbContext = mainDbContext;

		}
		public async Task<PagingResponseDto<GetDistrictDto>> GetDistrict()
		{
			var query = _mainDbContext.Districts
				.Select(x => new GetDistrictDto
				{
					Id = x.Id,
					Name = x.Name,
				});

			var totalCount = await query.CountAsync();

			var items = await query
				/* .Paging(request.PageIndex, request.Limit)*/
				.ToListAsync();

			return new PagingResponseDto<GetDistrictDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<RequestDistrictDto> PostDistrict(RequestDistrictDto request)
		{
			var district = new Entities.District
			{
				Name = request.Name,
				Province_id = request.Province_id
			};
			await _mainDbContext.Districts.AddAsync(district);
			await _mainDbContext.SaveChangesAsync();
			return new RequestDistrictDto
			{
				Name = district.Name,
			};
		}
	}
}
