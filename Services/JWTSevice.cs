using ERP_BACKEND.dtos;
using ERP_BACKEND.constracts;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;
using Microsoft.AspNetCore.Identity;




namespace ERP_BACKEND.services;

public class JWTservice :IJWTinterface
{

    private readonly AppDbContext _dbcontext;
    private readonly IConfiguration _configuration;

    public JWTservice( AppDbContext dbContext , IConfiguration configuration)
    {
       _dbcontext = dbContext;
       _configuration = configuration;
    }    

    public async Task<Loginresponse?>Authenticate (Loginrequest data )
    {
        if(string.IsNullOrEmpty(data.username) || string.IsNullOrEmpty(data.PasswordHash))
            return null;
        var useraccount = await _dbcontext.Users.FirstOrDefaultAsync(x=> x.Username == data.username);
        if (useraccount is null )
            return null;

    }
}
