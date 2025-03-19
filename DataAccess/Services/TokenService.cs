using Core.Identity;
using Core.RepositoriesInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _configuration= configuration;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:key"]));
        }
        public string CreateToken(AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.DisplayName)
			}; 

			var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = cred,
				Issuer = _configuration["Token:Issuer"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
