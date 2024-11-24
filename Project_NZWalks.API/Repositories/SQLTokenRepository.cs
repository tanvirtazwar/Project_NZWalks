using Microsoft.IdentityModel.Tokens;
using Project_NZWalks.API.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_NZWalks.API.Repositories;

public class SQLTokenRepository(IConfiguration configuration) 
    : ITokenRepository
{
    private readonly SymmetricSecurityKey _key =
       new(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]!));
    public string CreateJWTToken(AppUser user, List<string> roles)
    {
        //Create claim
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.NameId, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.GivenName, user.UserName!)
        ];
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        SigningCredentials credentials =
            new(_key, SecurityAlgorithms.HmacSha512Signature);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"]!,
            Audience = configuration["Jwt:Audience"]!
        };

        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.WriteToken(tokenHandler
            .CreateToken(tokenDescriptor));
    }
}
