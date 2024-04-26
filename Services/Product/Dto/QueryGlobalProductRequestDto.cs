using shopsport.CommonDto;

namespace shopsport.Services.Product.Dto
{
	public class QueryGlobalProductRequestDto 
	{
		public Guid Id { get; init; }
		public string Name { get; init; }
		public List<Guid> CategoryList { get; init; }
		public Guid CategoryParent_id { get; init; }

	}
}
