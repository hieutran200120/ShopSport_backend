namespace shopsport.Services.Product.Dto
{
	public class GetProductDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Image { get; set; }
		public decimal Price { get; set; }
		public decimal InportPrice { get; set; }
		public decimal PromotionPrice { get; set; }
		public int IsStatus { get; set; }
		public int Quantity { get; set; }
		public List<IFormFile> ImageFiles { get; init; }
		public CommonDto Category { get; set; }
		public CommonDto Supplier { get; set; }
	}
}
