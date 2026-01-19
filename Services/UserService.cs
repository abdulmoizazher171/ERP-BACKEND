using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.constracts;
using ERP_BACKEND.data;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ERP_BACKEND.services;

public class UserService : IUserInterface
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    public UserService(AppDbContext appdbContext , IPasswordHasher passwordHasher)
    {
        _dbContext = appdbContext;
        _passwordHasher = passwordHasher;
        
    }
    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<User?> Create(UserCreateDto dto)
    {
        if (dto is null)
        {
            return null;

        }

        else
        {
            var newuser =  new User
            {   
                USERNAME = dto.Username,
                ROLE =  dto.role,
                PASSWORD_HASH = _passwordHasher.Hash(dto.Password)
                
            };
             _dbContext.Users.Add(newuser);
             await _dbContext.SaveChangesAsync();

            // Return the newly created entity
            return newuser;

        }
    }
}