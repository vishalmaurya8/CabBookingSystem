using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CabBookingSystem.API.ScaffoldModels;
using Microsoft.IdentityModel.Tokens;

namespace CabBookingSystem.API.Repository
{
    public class SQLTokenRepository : IJWTTokenRepository
    {
        private readonly IConfiguration configuration;

        public SQLTokenRepository(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }
        
        public string CreateToken(User user)
        {
            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("userId", user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
