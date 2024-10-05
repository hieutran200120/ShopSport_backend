using shopsport.CommonDto;
using shopsport.Services.User.Dto;

namespace shopsport.Services.User
{
	public interface IUserService
	{
		Task<PagingResponseDto<UserDto>> GetCurrentUser(QueryGlobalUserRequestDto request);
		Task<UserDto> Register(RegisterDto request);
		Task<UserDto> Login(LoginRequestDto Request);
		Task<UserDto> DeleteUser(Guid Id);
		Task<UserDto> UpdateUser(Guid Id, RegisterDto request);
	}
}
