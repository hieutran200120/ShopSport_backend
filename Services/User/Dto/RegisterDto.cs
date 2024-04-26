namespace shopsport.Services.User.Dto
{
	public class RegisterDto
	{
		public string LastName { get; init; }
		public string FirstName { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }
		public string PhoneNumber { get; init; }
		public string Avatar { get; init; }
		public List<string> Role { get; init; }
		public IFormFile ImageFile { get; init; }
	}
}
