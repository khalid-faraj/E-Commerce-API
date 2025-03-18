using API.DTOs;
using API.Errors;
using Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : BaseApiController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
			_userManager = userManager;
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
				Token = "Will be Taken Here",
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
				Token = "Token",
				Email = registerDTO.Email,
			};
		}
    }
}
