using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Services.Province.Dto;

namespace shopsport.Services.Province
{
	public class ProvinceService : IProvinceService
	{
		private readonly MainDbContext _mainDbContext;
		public ProvinceService(MainDbContext mainDbContext)
		{
			_mainDbContext = mainDbContext;

		}
		public async Task<PagingResponseDto<GetProvinceDto>> GetProvince()
		{
			var query = _mainDbContext.Provinces
				.Select(x => new GetProvinceDto
				{
					Id = x.Id,
					Name = x.Name,
				});

			var totalCount = await query.CountAsync();

			var items = await query
				/* .Paging(request.PageIndex, request.Limit)*/
				.ToListAsync();

			return new PagingResponseDto<GetProvinceDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<RequestProvinceDto> PostProvince(RequestProvinceDto request)
		{
			var province = new Entities.Province
			{
				Name = request.Name,
			};
			await _mainDbContext.Provinces.AddAsync(province);
			await _mainDbContext.SaveChangesAsync();
			return new RequestProvinceDto
			{
				Name = province.Name,
			};
		}
	}
}
