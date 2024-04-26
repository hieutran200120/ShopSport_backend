namespace shopsport.Services.Supplier.Dto
{
	public class GetSupplierDto
	{
		public Guid Id { get; init; }
		public string Name { get; set; }
		public string Adress { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
	}
}
