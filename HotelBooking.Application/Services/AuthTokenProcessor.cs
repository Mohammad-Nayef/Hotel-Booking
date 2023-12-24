using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HotelBooking.Application.Services
{
    internal class AuthTokenProcessor : IAuthTokenProcessor
    {
        private readonly IConfiguration _config;

        public AuthTokenProcessor(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(UserDTO user)
        {
            var signingCredentials = GetSigningCredentials();
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            if (!double.TryParse(_config["Jwt:ExpirationMinutes"], out var expirationMinutes))
                throw new Exception("Invalid configuration for `Jwt:ExpirationMinutes`");

            var claims = new List<Claim>(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)))
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            return signingCredentials;
        }
    }
}
