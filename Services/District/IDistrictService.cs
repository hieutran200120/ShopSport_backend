using shopsport.CommonDto;
using shopsport.Services.District.Dto;

namespace shopsport.Services.District
{
	public interface IDistrictService
	{
		Task<PagingResponseDto<GetDistrictDto>> GetDistrict();
		Task<RequestDistrictDto> PostDistrict(RequestDistrictDto request);
	}
}
