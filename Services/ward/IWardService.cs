using shopsport.CommonDto;
using shopsport.Services.ward.Dto;

namespace shopsport.Services.ward
{
	public interface IWardService
	{
		Task<PagingResponseDto<GetWardDto>> GetWard();
		Task<RequestWardDto> PostWard(RequestWardDto request);
	}
}
