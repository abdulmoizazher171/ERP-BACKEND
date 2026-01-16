using Microsoft.AspNetCore.Mvc;
namespace ERP_BACKEND.Controllers;



[ApiController]
[Route("api/[controller]")]
public class PlacementController : Controller
{
    [HttpGet("")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Ok("");
    }
}