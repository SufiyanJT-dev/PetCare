using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Infrastucture.Persistance.Repository
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config) => _config = config;

        public string CreateAccessToken(User user, out DateTime expiresAt)
        {
            var key = _config["Jwt:Key"] ?? throw new InvalidOperationException();
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var minutes = int.Parse(_config["Jwt:AccessTokenMinutes"] ?? "15");

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.Name)
        };

            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            expiresAt = DateTime.UtcNow.AddMinutes(minutes);

            var token = new JwtSecurityToken(issuer, audience, claims, DateTime.UtcNow, expiresAt, creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken(out DateTime expiresAt)
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            var token = Convert.ToBase64String(bytes);
            var days = int.Parse(_config["Jwt:RefreshTokenDays"] ?? "30");
            expiresAt = DateTime.UtcNow.AddDays(days);
            return token;
        }
    }

}
