using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP_BACKEND.interfaces;
using ERP_BACKEND.dtos;
namespace ERP_BACKEND.Controllers;




[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlacementController : Controller
{
   private readonly IPlacementService _placementService;

    public PlacementController(IPlacementService placementService)
    {
        _placementService = placementService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlacementReadDto>>> GetPlacements()
    {
        return Ok(await _placementService.GetAllPlacementsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlacementReadDto>> GetPlacement(int id)
    {
        var placement = await _placementService.GetPlacementByIdAsync(id);
        return placement == null ? NotFound() : Ok(placement);
    }

    [HttpPost]
    public async Task<ActionResult<PlacementReadDto>> PostPlacement(PlacementCreateDto createDto)
    {
        var result = await _placementService.CreatePlacementAsync(createDto);
        return CreatedAtAction(nameof(GetPlacement), new { id = result.AssetPlacementId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlacement(int id, PlacementCreateDto updateDto)
    {
        var success = await _placementService.UpdatePlacementAsync(id, updateDto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlacement(int id)
    {
        var success = await _placementService.DeletePlacementAsync(id);
        return success ? NoContent() : NotFound();
    }
}