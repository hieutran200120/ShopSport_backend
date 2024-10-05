using shopsport.Services.Order.Dto;
using shopsport.Services.VnPay.Dto;

namespace shopsport.Services.VnPay
{
    public interface IVnPayService
    {
		Task<string> CreatePaymentUrl(OrderRequestDto model, HttpContext context);
		PaymentResponseModel PaymentExecute(IQueryCollection collections);
	}
}
