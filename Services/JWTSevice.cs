using ERP_BACKEND.dtos;
using ERP_BACKEND.constracts;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Azure.Core;
using System.Text;
using System.Reflection.Metadata.Ecma335;




namespace ERP_BACKEND.services;

public class JWTservice : IJWTinterface
{

    private readonly AppDbContext _dbcontext;
    private readonly IConfiguration _configuration;

    private readonly IPasswordHasher _passwordhaser;

    public JWTservice(AppDbContext dbContext, IConfiguration configuration , IPasswordHasher passwordHasher)
    {
        _dbcontext = dbContext;
        _configuration = configuration;
        _passwordhaser = passwordHasher;
    }

    public async Task<Loginresponse?> Authenticate(Loginrequest data)
    {
        if (string.IsNullOrEmpty(data.username) || string.IsNullOrEmpty(data.Password))
            return null;
        var useraccount = await _dbcontext.Users.FirstOrDefaultAsync(x => x.USERNAME == data.username);
        if (useraccount is null )
            return null;
         if (_passwordhaser.Verify(useraccount.PASSWORD_HASH, data.Password))
        {
            var issuer = _configuration["JWTConfiguration:Issuer"];
            var audience = _configuration["JWTConfiguration:Audience"];
            var key = _configuration["JWTConfiguration:Key"];
            var tokenvalidityMins = _configuration.GetValue<int>("JWTConfiguration:ValiditiyTime");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenvalidityMins);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Name , data.username)
            }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha384Signature

                ),

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new Loginresponse(
                useraccount.ROLE,
                accessToken,
                (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            );
        }
        else
        {
            return null;
        }
    }


}
