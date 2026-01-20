using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_BACKEND.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<readStoreDto>>> GetStores()
    {
        return Ok(await _storeService.GetAllStoresAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<readStoreDto>> GetStore(int id)
    {
        var store = await _storeService.GetStoreByIdAsync(id);
        if (store == null) return NotFound();
        return Ok(store);
    }

    [HttpPost]
    public async Task<ActionResult<readStoreDto>> PostStore(createStoreDto storeDto)
    {
        var createdStore = await _storeService.CreateStoreAsync(storeDto);
        return CreatedAtAction(nameof(GetStore), new { id = createdStore.StoreId }, createdStore);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStore(int id, createStoreDto storeDto)
    {
        var success = await _storeService.UpdateStoreAsync(id, storeDto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(int id)
    {
        var success = await _storeService.DeleteStoreAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}