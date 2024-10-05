using shopsport.Services.Product.Dto;

namespace shopsport.Services.Order.Dto
{
	public class ReportOrderProduct
	{
		public Guid Id { get; set; } 
		public ProductDto Product { get; set; }
		public int TotalQuantity { get; set; }
	}
}
