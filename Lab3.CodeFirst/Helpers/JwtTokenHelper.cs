using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab3.CodeFirst.Models;
using Microsoft.IdentityModel.Tokens;

namespace Lab3.CodeFirst.Helpers;

public class JwtTokenHelper
{
    private readonly IConfiguration _configuration;

    public JwtTokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("email-address", user.Email),
            new Claim("position", ((int)user.Position).ToString()),
            new Claim("user-id", user.Id.ToString())
        };

        var tokenLifetime = TimeSpan.Parse(_configuration["Jwt:TokenLifetime"]);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.Add(tokenLifetime),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}