using ERP_BACKEND.data;
using ERP_BACKEND.dtos;
using ERP_BACKEND.constracts;
using Microsoft.EntityFrameworkCore;


public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<readCategoryDto>> GetAllCategoriesAsync()
    {
        return await _context.Category
            .Select(c => new readCategoryDto(c.CATEGORY_ID, c.CATEGORY_NAME))
            .ToListAsync();
    }

    public async Task<readCategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Category.FindAsync(id);
        
        if (category == null) return null;

        return new readCategoryDto(category.CATEGORY_ID, category.CATEGORY_NAME);
    }

    public async Task<readCategoryDto> CreateCategoryAsync(createCategoryDto categoryDto)
    {
        // Map DTO to Entity
        var category = new Category 
        { 
            CATEGORY_NAME = categoryDto.CategoryName 
        };

        _context.Category.Add(category);
        await _context.SaveChangesAsync();
        
        // Return a new record with the database-generated ID
        return new readCategoryDto(category.CATEGORY_ID, category.CATEGORY_NAME);
    }

    public async Task<bool> UpdateCategoryAsync(int id, createCategoryDto categoryDto)
    {
        var category = await _context.Category.FindAsync(id);
        if (category == null) return false;

        // Update the entity properties from the record
        category.CATEGORY_NAME = categoryDto.CategoryName;

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Category.FindAsync(id);
        if (category == null) return false;

        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}