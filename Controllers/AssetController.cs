using Microsoft.AspNetCore.Mvc;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using ERP_BACKEND.constracts;

namespace ERP_BACKEND.Controllers;



[ApiController]
[Route("api/[controller]")]
public class AssetsController : Controller
{

    private readonly IAsset _IAsset;

    private AssetsController(IAsset asset)
    {
        _IAsset = asset;
    }

    [HttpGet("all")]
    public async Task<IActionResult> Getall()
    {
        var result = await _IAsset.GetAllAssetsAsync();
        return Ok(result);
       
    }
}