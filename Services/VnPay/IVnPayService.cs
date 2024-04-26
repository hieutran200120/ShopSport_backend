using shopsport.Services.Order.Dto;
using shopsport.Services.VnPay.Dto;

namespace shopsport.Services.VnPay
{
    public interface IVnPayService
    {
		string CreatePaymentUrl(OrderRequestDto model, HttpContext context);
		PaymentResponseModel PaymentExecute(IQueryCollection collections);
	}
}
