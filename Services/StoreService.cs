using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;

namespace ERP_BACKEND.services;

public class StoreService : IStoreService
{
    private readonly AppDbContext _context;

    public StoreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<readStoreDto>> GetAllStoresAsync()
    {
        return await _context.Store
            .Select(s => new readStoreDto(s.STORE_ID, s.STORE_NAME))
            .ToListAsync();
    }

    public async Task<readStoreDto?> GetStoreByIdAsync(int id)
    {
        var store = await _context.Store.FindAsync(id);
        if (store == null) return null;

        return new readStoreDto(store.STORE_ID, store.STORE_NAME);
    }

    public async Task<readStoreDto> CreateStoreAsync(createStoreDto storeDto)
    {
        var store = new Store
        {
            STORE_NAME = storeDto.StoreName
        };

        _context.Store.Add(store);
        await _context.SaveChangesAsync();

        return new readStoreDto(store.STORE_ID, store.STORE_NAME);
    }

    public async Task<bool> UpdateStoreAsync(int id, createStoreDto storeDto)
    {
        var store = await _context.Store.FindAsync(id);
        if (store == null) return false;

        store.STORE_NAME = storeDto.StoreName;

        _context.Entry(store).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteStoreAsync(int id)
    {
        var store = await _context.Store.FindAsync(id);
        if (store == null) return false;

        _context.Store.Remove(store);
        await _context.SaveChangesAsync();
        return true;
    }
}