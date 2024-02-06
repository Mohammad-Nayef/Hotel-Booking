using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Configurations;
using HotelBooking.Domain.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelBooking.Application.Services
{
    /// <inheritdoc cref="IAuthTokenProcessor"/>
    internal class AuthTokenProcessor : IAuthTokenProcessor
    {
        private readonly JwtConfigurations _jwtConfig;

        public AuthTokenProcessor(IOptions<JwtConfigurations> jwtOptions)
        {
            _jwtConfig = jwtOptions.Value;
        }

        public string GenerateToken(UserDTO user)
        {
            var signingCredentials = GetSigningCredentials();
            var issuer = _jwtConfig.Issuer;
            var audience = _jwtConfig.Audience;
            var expirationMinutes = _jwtConfig.ExpirationMinutes;

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
                Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            return signingCredentials;
        }
    }
}
