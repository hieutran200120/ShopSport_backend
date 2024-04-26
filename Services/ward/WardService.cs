using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Services.ward.Dto;

namespace shopsport.Services.ward
{
	public class WardService : IWardService
	{
		private readonly MainDbContext _mainDbContext;
		public WardService(MainDbContext mainDbContext)
		{
			_mainDbContext = mainDbContext;

		}
		public async Task<PagingResponseDto<GetWardDto>> GetWard()
		{
			var query = _mainDbContext.Wards
				.Select(x => new GetWardDto
				{
					Id = x.Id,
					Name = x.Name,
				});

			var totalCount = await query.CountAsync();

			var items = await query
				/* .Paging(request.PageIndex, request.Limit)*/
				.ToListAsync();

			return new PagingResponseDto<GetWardDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<RequestWardDto> PostWard(RequestWardDto request)
		{
			var ward = new Entities.Ward
			{
				Name = request.Name,
				District_id = request.District_id
			};
			await _mainDbContext.Wards.AddAsync(ward);
			await _mainDbContext.SaveChangesAsync();
			return new RequestWardDto
			{
				Name = ward.Name,
			};
		}
	}
}
