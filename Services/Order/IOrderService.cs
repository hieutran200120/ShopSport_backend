using Microsoft.AspNetCore.Mvc;
using shopsport.CommonDto;
using shopsport.Services.Order.Dto;

namespace shopsport.Services.Order
{
	public interface IOrderService
	{
		Task<OrderRequestDto> AddOrderItems(OrderRequestDto orderRequest);
		Task<PagingResponseDto<OrderRespon>> GetOrderId(QueryGlobalOrderRequestDto request);
		Task<PagingResponseDto<OrderRespon>> GetOrder(QueryGlobalOrderRequestDto request);
		Task<changeStatusDto> UpdateOrder(Guid Id, changeStatusDto request);
	}
}
