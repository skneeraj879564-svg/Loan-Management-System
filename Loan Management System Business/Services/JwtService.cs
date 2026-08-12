using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Loan_Management_System_Business.Services
{
    public class JwtService :IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(
            ApplicationUser user)
        {
            // Get JWT settings from appsettings.json
            var key = _configuration["JwtSettings:Key"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var expirationMinutes =
                Convert.ToInt32(
                    _configuration["JwtSettings:ExpirationMinutes"]);

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);

            // Claims
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName ?? string.Empty),

                new Claim(
                    ClaimTypes.Email,
                    user.Email ?? string.Empty),

                new Claim(
                    "FullName",
                    user.FullName)
            };

            // Add roles
            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role));
            }

            // Security key
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!));

            // Credentials
            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            // Create token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    expirationMinutes),
                signingCredentials: credentials);

            // Convert token to string
            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
