using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Commands
{
    public interface ITokenProvider
    {
        string GenerateJwtToken(IList<Claim> claims);
        string GenerateRefreshToken();
    }

    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        public TokenProvider(IConfiguration configuration,
          IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string GenerateJwtToken(IList<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SigningKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256) /* HmacSha256Signature */
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string Issuer
        {
            get
            {
                string scheme = _httpContext.Request.Scheme;
                string host = _httpContext.Request.Host.Value;
                return $"{scheme}://{host}";
            }
        }
    }
}
