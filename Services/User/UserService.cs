using Microsoft.EntityFrameworkCore;
using shopsport.CommonDto;
using shopsport.Exceptions;
using shopsport.Infrastructure.Auth;
using shopsport.LinQ;
using shopsport.Services.Product.Dto;
using shopsport.Services.User.Dto;

namespace shopsport.Services.User
{
	public class UserService : IUserService
	{
		private readonly MainDbContext _mainDbContext;
		private readonly IAuthService _authService;
		private readonly ICurrentUser _currentUser;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserService(MainDbContext mainDbContext, IAuthService authService, ICurrentUser currentUser, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_mainDbContext = mainDbContext;
			_authService = authService;
			_currentUser = currentUser;
			_hostEnvironment = hostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<PagingResponseDto<UserDto>> GetCurrentUser(QueryGlobalUserRequestDto request)
		{
			var userQuery = _mainDbContext.Users
				.WhereIf(request.Id != Guid.Empty, x => x.Id.Equals(request.Id))
				.Select(x => new UserDto
				{
					Id  = x.Id,
					Email = x.Email,
					FirstName = x.FirstName,
					LastName = x.LastName,
					PhoneNumber = x.PhoneNumber,
					Roles=x.Roles,
					Avatar = x.Avatar,
					Token = _authService.GenerateToken(x),
				});

			var totalCount = await userQuery.CountAsync();

			var items = await userQuery
				.ToListAsync();

			return new PagingResponseDto<UserDto>
			{
				Items = items,
				TotalCount = totalCount
			};
		}
		public async Task<UserDto> Register(RegisterDto request)
		{
			var newUser = new Entities.User
			{
				LastName = request.LastName,
				FirstName = request.FirstName,
				Email = request.Email,
				Password = _authService.HashPassword(request.Password),
				PhoneNumber = request.PhoneNumber,
				Avatar = await SaveImage(request.ImageFile),
				Roles =  request.Role
			};
			_mainDbContext.Users.Add(newUser);
			await _mainDbContext.SaveChangesAsync();
			return new UserDto
			{
				LastName = newUser.LastName,
				FirstName = newUser.FirstName,
				Email = newUser.Email,
				PhoneNumber = newUser.PhoneNumber,
				Roles = newUser.Roles
			};
		}
		public async Task<UserDto> Login(LoginRequestDto Request)
		{

			var user = await _mainDbContext.Users.FirstOrDefaultAsync(a => a.Email == Request.Email);
			if (user == null)
			{
				throw new Exception("Không có email này");
			}
			bool checkPassword = _authService.VerifyPassword(Request.Password, user.Password);
			if (!checkPassword)
			{
				throw new Exception("mật khẩu không hợp lệ");
			}
			return new UserDto
			{
				Id=user.Id,
				LastName = user.LastName,
				FirstName = user.FirstName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Roles = user.Roles,
				Avatar = user.Avatar,
				Token = _authService.GenerateToken(user)
			};
		}
		public async Task<UserDto> DeleteUser(Guid Id)
		{
			var user = _mainDbContext.Users.FirstOrDefault(x => x.Id == Id);
			_mainDbContext.Users.Remove(user);
			await _mainDbContext.SaveChangesAsync();

			return new UserDto
			{
				Id = user.Id,
				LastName= user.LastName,
				FirstName= user.FirstName,
				Email = user.Email,
				PhoneNumber= user.PhoneNumber,
				Roles = user.Roles,
				Avatar = user.Avatar,

			};
		}
		public async Task<UserDto> UpdateUser(Guid Id, RegisterDto request)
		{
			var user = _mainDbContext.Users.FirstOrDefault(x => x.Id == Id);
			if (user == null)
			{
				throw new RestException(System.Net.HttpStatusCode.NotFound, "No article");
			}
			user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.Email = request.Email;
			user.PhoneNumber = request.PhoneNumber;
			user.Roles = request.Role;
			user.Avatar = await SaveImage(request.ImageFile);
			await _mainDbContext.SaveChangesAsync();
			return new UserDto
			{
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email,
			PhoneNumber = user.PhoneNumber,
			Roles = user.Roles,
			Avatar = user.Avatar,
			};
		}
		public async Task<string> SaveImage(IFormFile imageFile)
		{
			if (imageFile == null || imageFile.Length == 0)
			{
				return null; 
			}

			string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
			imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
			var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
			using (var fileStream = new FileStream(imagePath, FileMode.Create))
			{
				await imageFile.CopyToAsync(fileStream);
			}
			return imageName;
		}
	}
}
