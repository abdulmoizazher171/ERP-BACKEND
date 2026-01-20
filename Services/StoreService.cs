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

    public async Task<IEnumerable<StoreDto>> GetAllStoresAsync()
    {
        return await _context.Stores
            .Select(s => new StoreDto(s.STORE_ID, s.STORE_NAME))
            .ToListAsync();
    }

    public async Task<StoreDto?> GetStoreByIdAsync(int id)
    {
        var store = await _context.Stores.FindAsync(id);
        if (store == null) return null;

        return new StoreDto(store.STORE_ID, store.STORE_NAME);
    }

    public async Task<StoreDto> CreateStoreAsync(StoreDto storeDto)
    {
        var store = new Store
        {
            STORE_NAME = storeDto.StoreName
        };

        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        return new StoreDto(store.STORE_ID, store.STORE_NAME);
    }

    public async Task<bool> UpdateStoreAsync(int id, StoreDto storeDto)
    {
        var store = await _context.Stores.FindAsync(id);
        if (store == null) return false;

        store.STORE_NAME = storeDto.StoreName;

        _context.Entry(store).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteStoreAsync(int id)
    {
        var store = await _context.Stores.FindAsync(id);
        if (store == null) return false;

        _context.Stores.Remove(store);
        await _context.SaveChangesAsync();
        return true;
    }
}