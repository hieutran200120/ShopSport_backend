using shopsport.Entities;
using shopsport.Services.Product.Dto;

namespace shopsport.Services.Returns.Dto
{
	public class ReturnProductDto
	{
		public Guid Order_id { get; set; }
		public Guid Product_id { get; set; }
		public int Quantity { get; set; }
		public string Reason { get; set; }
		public decimal Price { get; set; }
	}
}
