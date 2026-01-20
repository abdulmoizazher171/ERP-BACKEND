using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_BACKEND.Controllers;


[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<readCategoryDto>>> GetCategories()
    {
        return Ok(await _categoryService.GetAllCategoriesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<readCategoryDto>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return category == null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<readCategoryDto>> PostCategory(createCategoryDto categoryDto)
    {
        var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
        return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryId }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, createCategoryDto categoryDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, categoryDto);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        return result ? NoContent() : NotFound();
    }
}