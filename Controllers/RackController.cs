using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_BACKEND.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RacksController : ControllerBase
{
    private readonly IRackService _rackService;

    public RacksController(IRackService rackService)
    {
        _rackService = rackService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<readRackDto>>> GetRacks()
    {
        var racks = await _rackService.GetAllRacksAsync();
        return Ok(racks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<readRackDto>> GetRack(int id)
    {
        var rack = await _rackService.GetRackByIdAsync(id);
        if (rack == null) return NotFound();
        return Ok(rack);
    }

    [HttpPost]
    public async Task<ActionResult<readRackDto>> PostRack(createRackDto rackDto)
    {
        var createdRack = await _rackService.CreateRackAsync(rackDto);
        return CreatedAtAction(nameof(GetRack), new { id = createdRack.RackId }, createdRack);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRack(int id, createRackDto rackDto)
    {
        var success = await _rackService.UpdateRackAsync(id, rackDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRack(int id)
    {
        var success = await _rackService.DeleteRackAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}