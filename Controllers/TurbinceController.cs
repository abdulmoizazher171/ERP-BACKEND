using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_BACKEND.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TurbinesController : ControllerBase
{
    private readonly ITurbineService _turbineService;

    public TurbinesController(ITurbineService turbineService)
    {
        _turbineService = turbineService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<readTurbineDto>>> GetTurbines()
    {
        return Ok(await _turbineService.GetAllTurbinesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<readTurbineDto>> GetTurbine(int id)
    {
        var turbine = await _turbineService.GetTurbineByIdAsync(id);
        if (turbine == null) return NotFound();
        return Ok(turbine);
    }

    [HttpPost]
    public async Task<ActionResult<readTurbineDto>> PostTurbine(createTurbineDto turbineDto)
    {
        var created = await _turbineService.CreateTurbineAsync(turbineDto);
        return CreatedAtAction(nameof(GetTurbine), new { id = created.TurbineId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTurbine(int id, createTurbineDto turbineDto)
    {
        var success = await _turbineService.UpdateTurbineAsync(id, turbineDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTurbine(int id)
    {
        var success = await _turbineService.DeleteTurbineAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}