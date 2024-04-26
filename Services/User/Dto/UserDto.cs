using shopsport.Entitycommon;

namespace shopsport.Services.User.Dto
{
	public class UserDto:IAudiInfo
	{
		public Guid Id { get; init; }
		public string LastName { get; init; }
		public string FirstName { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }
		public string PhoneNumber { get; init; }
		public string Avatar { get; init; }
		public string Token { get; init; }
		public List<string> Roles { get; init; }

	}
}
