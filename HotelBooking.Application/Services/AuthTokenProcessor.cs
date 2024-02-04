using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HotelBooking.Application.Services
{
    /// <inheritdoc cref="IAuthTokenProcessor"/>
    internal class AuthTokenProcessor : IAuthTokenProcessor
    {
        private readonly IConfiguration _config;

        public AuthTokenProcessor(IConfiguration config)
        {
            _config = config;
        }

        internal TokenValidationParameters TokenValidationParameters
        {
            get => new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        }

        public string GenerateToken(UserDTO user)
        {
            var signingCredentials = GetSigningCredentials();
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            if (!double.TryParse(_config["Jwt:ExpirationMinutes"], out var expirationMinutes))
                throw new Exception("Invalid configuration for `Jwt:ExpirationMinutes`");

            var claims = GetClaims(user);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static List<Claim> GetClaims(UserDTO user)
        {
            return new List<Claim>(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)))
            {
                new(ClaimTypes.Name, user.Id.ToString())
            };
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
