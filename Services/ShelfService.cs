using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;

namespace ERP_BACKEND.services;

public class ShelfService : IShelfService
{
    private readonly AppDbContext _context;

    public ShelfService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<readShelfDto>> GetAllShelfsAsync()
    {
        return await _context.Shelf
            .Select(s => new readShelfDto(s.SHELF_ID, s.SHELF_NAME))
            .ToListAsync();
    }

    public async Task<readShelfDto?> GetShelfByIdAsync(int id)
    {
        var Shelf = await _context.Shelf.FindAsync(id);
        if (Shelf == null) return null;

        return new readShelfDto(Shelf.SHELF_ID, Shelf.SHELF_NAME);
    }

    public async Task<readShelfDto> CreateShelfAsync(createShelfDto ShelfDto)
    {
        var Shelf = new Shelf
        {
            SHELF_NAME = ShelfDto.shelfName
        };

        _context.Shelf.Add(Shelf);
        await _context.SaveChangesAsync();

        return new readShelfDto(Shelf.SHELF_ID, Shelf.SHELF_NAME);
    }

    public async Task<bool> UpdateShelfAsync(int id, createShelfDto ShelfDto)
    {
        var Shelf = await _context.Shelf.FindAsync(id);
        if (Shelf == null) return false;

        Shelf.SHELF_NAME = ShelfDto.shelfName;

        _context.Entry(Shelf).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteShelfAsync(int id)
    {
        var Shelf = await _context.Shelf.FindAsync(id);
        if (Shelf == null) return false;

        _context.Shelf.Remove(Shelf);
        await _context.SaveChangesAsync();
        return true;
    }
}