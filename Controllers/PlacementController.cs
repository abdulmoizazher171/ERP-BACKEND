using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ERP_BACKEND.Controllers;



[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlacementController : Controller
{
    [HttpGet("all")]
    public IEnumerable<WeatherForecast> Get()
    {
        return (IEnumerable<WeatherForecast>)Ok("");
    }
}