
using System.Runtime.InteropServices;
using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using ERP_BACKEND.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace ERP_BACKEND.Controllers;



[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : Controller
{
    private readonly IJWTinterface _IjWTservice;
    private readonly IUserInterface _Iuserinterface;
    public UserController (IJWTinterface IjWTservice , IUserInterface iuserinterface) {
      _IjWTservice = IjWTservice;
      _Iuserinterface = iuserinterface;
    }

    [HttpGet("")]
    public IEnumerable<WeatherForecast> Get()
    {
        return (IEnumerable<WeatherForecast>)Ok("");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<Loginresponse>> login (Loginrequest request)
    {
        var result = await _IjWTservice.Authenticate(request);
        if (result is null)
        {
            return Unauthorized();
        }
        return result;
    }

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<ActionResult<User?>> create (UserCreateDto dto)
    {
        var result = await _Iuserinterface.Create(dto);
        
        return Ok(result);
    }
}