namespace shopsport.Services.Returns.Dto
{
	public class ReturnItemDto
	{
		public string Name { get; set; }
		public int Quantity { get; set; }
		public string Reason { get; set; }
		public decimal Price { get; set; }
		public List<string> Image { get; set; }
		public string Email { get; set; }
	}
}
