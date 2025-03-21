using API.DTOs;
using API.Errors;
using Core.Identity;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : BaseApiController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		public AccountsController(UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			ITokenService tokenService)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_tokenService = tokenService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDTO>> Login(LogInDTO logInDTO)
		{
			var user = await _userManager.FindByEmailAsync(logInDTO.Email);
			if (user == null) return Unauthorized(new ApiResponse(401));

			var result = await _signInManager.CheckPasswordSignInAsync(user, logInDTO.Password, false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

			return new UserDTO
			{
				Email = logInDTO.Email,
				Token = _tokenService.CreateToken(user),
				DisplayName = user.DisplayName
			};
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
		{
			var user = new AppUser
			{
				DisplayName = registerDTO.DisplayName,
				Email = registerDTO.Email,
				UserName = registerDTO.Email
			};

			var result = await _userManager.CreateAsync(user, registerDTO.Password);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400));

			return new UserDTO
			{
				DisplayName = registerDTO.DisplayName,
				Token = "tttt",
				Email = registerDTO.Email,
			};
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDTO>> GetCurrentUser()
		{
			var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
			var user = await _userManager.FindByNameAsync(email);
			return new UserDTO
			{
				Email = user.Email,
				Token = _tokenService.CreateToken(user),
				DisplayName = user.DisplayName
			};
		}
		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> CheckEmailAsync([FromQuery] string email)
		{
			return await _userManager.FindByEmailAsync(email) != null;
		}

		[HttpGet("address")]
		public async Task<ActionResult<Address>> GetUserAddress()
		{
			var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
			var user = await _userManager.FindByEmailAsync(email);
			return user.Address;

		}
	}
}
