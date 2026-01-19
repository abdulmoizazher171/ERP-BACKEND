using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ERP_BACKEND.interfaces;

public  interface IUserInterface
{
    public Task<User?> Create (UserCreateDto dto);
}