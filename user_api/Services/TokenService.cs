
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using user_api.Models;

namespace user_api.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {   
        // SET JWT TOKEN CLAIMS
        Claim[] claims = new Claim[]
        {   
            new Claim("id", user.Id),
            new Claim("username", user.UserName),
            new Claim("normalizedUsername", user.NormalizedUserName),
            new Claim("birthDate", user.BirthDate)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("8567DFDAFYUIASDF876SDAGFUYHJ")
        );

        SigningCredentials signingCredentials = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256
        );

        JwtSecurityToken token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}