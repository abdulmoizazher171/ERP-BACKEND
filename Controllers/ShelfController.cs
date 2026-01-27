using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_BACKEND.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ShelfsController : ControllerBase
{
    private readonly IShelfService _ShelfService;

    public ShelfsController(IShelfService ShelfService)
    {
        _ShelfService = ShelfService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<readShelfDto>>> GetShelfs()
    {
        return Ok(await _ShelfService.GetAllShelfsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<readShelfDto>> GetShelf(int id)
    {
        var Shelf = await _ShelfService.GetShelfByIdAsync(id);
        if (Shelf == null) return NotFound();
        return Ok(Shelf);
    }

    [HttpPost]
    public async Task<ActionResult<readShelfDto>> PostShelf([FromBody] createShelfDto ShelfDto)
    {
        if (ShelfDto == null || string.IsNullOrEmpty(ShelfDto.shelfName))
    {
        return BadRequest("Invalid shelf data. shelfName is required.");
    }

    var createdShelf = await _ShelfService.CreateShelfAsync(ShelfDto);
    return CreatedAtAction(nameof(GetShelf), new { id = createdShelf.Shelf_Id }, createdShelf);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShelf(int id, createShelfDto ShelfDto)
    {
        var success = await _ShelfService.UpdateShelfAsync(id, ShelfDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShelf(int id)
    {
        var success = await _ShelfService.DeleteShelfAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}