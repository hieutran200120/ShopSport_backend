using shopsport.CommonDto;
using shopsport.Services.Province.Dto;

namespace shopsport.Services.Province
{
	public interface IProvinceService
	{
		Task<PagingResponseDto<GetProvinceDto>> GetProvince();
		Task<RequestProvinceDto> PostProvince(RequestProvinceDto request);
	}
}
