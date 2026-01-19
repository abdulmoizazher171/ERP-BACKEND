using Microsoft.AspNetCore.Mvc;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using ERP_BACKEND.constracts;
using Microsoft.AspNetCore.Authorization;

namespace ERP_BACKEND.Controllers;



[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssetsController : Controller
{

    private readonly IAsset _IAsset;

    public AssetsController(IAsset asset)
    {
        _IAsset = asset;
    }

    [HttpGet("all")]
    public async Task<IActionResult> Getall()
    {
        var result = await _IAsset.GetAllAssetsAsync();
        return Ok(result);
       
    }

    [HttpPost("crete")]

    public async Task<IActionResult> create(AssetCreateDto dto)
    {
        
        var result = await _IAsset.AddAssetAsync(dto);
        return (IActionResult)result;


    }
}