using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
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
		private readonly IMapper _mapper;

		public AccountsController(UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			ITokenService tokenService,
			IMapper mapper)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_tokenService = tokenService;
			_mapper = mapper;
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
			var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

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
		public async Task<ActionResult<AddressDTO>> GetUserAddress()
		{
			var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
			return _mapper.Map<Address, AddressDTO>(user.Address);

		}

		[Authorize]
		[HttpPut("address")]
		public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
		{
			var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
			user.Address = _mapper.Map<AddressDTO, Address>(address);
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded) return Ok(_mapper.Map<AddressDTO>(user.Address));
			return BadRequest("Problem updating the user");
		}

	}
}
