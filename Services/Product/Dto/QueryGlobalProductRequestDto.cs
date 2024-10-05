using shopsport.CommonDto;

namespace shopsport.Services.Product.Dto
{
	public class QueryGlobalProductRequestDto :PagingRequestDto
	{
		public Guid Id { get; init; }
		public string Name { get; init; }
		public List<Guid> CategoryList { get; init; }
		public List<Guid> SupplierList { get; init; }
		public Guid CategoryParent_id { get; init; }
		public decimal PriceMin { get; init; }
		public decimal PriceMax { get; init;}
		public int IsStatus { get; init; }

	}
}
