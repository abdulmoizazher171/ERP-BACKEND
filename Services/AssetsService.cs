using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.constracts;
using ERP_BACKEND.data;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
namespace ERP_BACKEND.services;

public class AssetService : IAsset
{
    private readonly AppDbContext _context;

    public AssetService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
    {
        // We use .Include to join the related tables (Category, Turbine, etc.)
        return await _context.Assets
            .Include(a => a.Category)
            .Include(a => a.Turbine)
            .ToListAsync();
    }

    public async Task<Asset?> GetAssetByIdAsync(int id)
    {
        return await _context.Assets
            .Include(a => a.Attachments)
            .FirstOrDefaultAsync(a => a.ITEM_ID == id);
    }

    public async Task<Asset> AddAssetAsync(AssetCreateDto dto)
    {
        var newasset = new Asset
        {
            ITEM_NAME =  dto.ItemName,
            TURBINE_ID =  dto.TurbineId,
            STORE_ID = dto.StoreId,
            RACK_ID =  dto.rackid,
            DESCRIPTION = dto.Description,
            CATEGORY_ID = dto.CategoryId,

            
            

            
        };
        _context.Assets.Add(newasset);
        await _context.SaveChangesAsync();
        return newasset;
    }

    // ... other methods follow the same pattern
}