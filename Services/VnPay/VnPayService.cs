using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using shopsport.Entities;
using shopsport.Migrations;
using shopsport.Services.Order.Dto;
using shopsport.Services.VnPay.Dto;

namespace shopsport.Services.VnPay
{
	public class VnPayService : IVnPayService
	{
		private readonly IConfiguration _configuration;
		private readonly MainDbContext _mainDbContext;

		public VnPayService(IConfiguration configuration, MainDbContext mainDbContext)
		{
			_configuration = configuration;
			_mainDbContext = mainDbContext;
		}

		public string CreatePaymentUrl(OrderRequestDto model, HttpContext context)
		{
			var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
			var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
			var tick = DateTime.Now.Ticks.ToString();
			var pay = new VnPayLibrary();
			var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];
			pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
			pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
			pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
			pay.AddRequestData("vnp_Amount", ((int)model.Order.Price * 100).ToString());
			pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
			pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
			pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
			pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
			pay.AddRequestData("vnp_OrderInfo", $"{model.Order.FirstName} {model.Order.Note} {model.Order.Price}");
			pay.AddRequestData("vnp_OrderType", "payment");
			pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
			pay.AddRequestData("vnp_TxnRef", tick);
			var paymentUrl =
				pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

			var order = new Entities.Order
			{
				FirstName = model.Order.FirstName,
				LastName = model.Order.LastName,
				Address = model.Order.Address,
				Note = model.Order.Note,
				PhoneNumber = model.Order.PhoneNumber,
				Price = model.Order.Price,
				Status = model.Order.Status,
				/*IsPay=model.Order.IsPay,*/
				Province_id = model.Order.Province_id,
				District_id = model.Order.District_id,
				Ward_id = model.Order.Ward_id,
				User_id = model.Order.User_id
			};

			 _mainDbContext.Orders.AddAsync(order);
			 _mainDbContext.SaveChangesAsync();

			if (model.OrderItems != null && model.OrderItems.Any())
			{
				foreach (var item in model.OrderItems)
				{
					var orderItem = new OrderDetail
					{
						OrderId = order.Id,
						ProductId = item.ProductId,
						Price = item.Price,
						Quantity = item.Quantity
					};

					_mainDbContext.OrderDetails.Add(orderItem);
				}

				 _mainDbContext.SaveChangesAsync();
			}

			return paymentUrl;
		}

		public PaymentResponseModel PaymentExecute(IQueryCollection collections)
		{
			var pay = new VnPayLibrary();
			var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

			return response;
		}
	}
}
