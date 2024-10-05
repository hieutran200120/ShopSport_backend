

using shopsport.CommonDto;
using shopsport.Services.Returns.Dto;

namespace shopsport.Services.Returns
{
	public interface IReturnSerive
	{
		Task<ReturnDto> AddReturnItems(ReturnDto orderRequest);
		Task<PagingResponseDto<ReturnItemDto>> GetProduct(QueryGlobalProductReturnRequestDto request);
	}
}
