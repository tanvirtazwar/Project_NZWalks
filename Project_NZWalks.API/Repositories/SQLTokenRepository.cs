﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_NZWalks.API.Repositories;

public class SQLTokenRepository(IConfiguration configuration) 
    : ITokenRepository
{
    public string CreateJWTToken(IdentityUser user, List<string> roles)
    {
        //Create claim
        var claims = new List<Claim>();
        if (user.Email != null)
        {
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
        }
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(40),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}