using shopsport.CommonDto;
using shopsport.Services.User.Dto;

namespace shopsport.Services.User
{
	public interface IUserService
	{
		Task<PagingResponseDto<UserDto>> GetCurrentUser();
		Task<UserDto> Register(RegisterDto request);
		Task<UserDto> Login(LoginRequestDto Request);
	}
}
