using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;
namespace ERP_BACKEND.Services;

public class PlacementService : IPlacementService
{
    private readonly AppDbContext _context;

    public PlacementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PlacementReadDto>> GetAllPlacementsAsync()
    {
        return await _context.Asset_Placement
            .Select(p => new PlacementReadDto(
                p.ASSET_PLACEMENT_ID,
                p.ITEM_ID,
                p.SHELF_ID,
                p.RACK_ID,
                p.PLACED_DATE,
                p.PLACED_BY
            )).ToListAsync();
    }

    public async Task<PlacementReadDto?> GetPlacementByIdAsync(int id)
    {
        var p = await _context.Asset_Placement.FindAsync(id);
        if (p == null) return null;

        return new PlacementReadDto(
            p.ASSET_PLACEMENT_ID,
            p.ITEM_ID,
            p.SHELF_ID,
            p.RACK_ID,
            p.PLACED_DATE,
            p.PLACED_BY
        );
    }

   public async Task<PlacementReadDto> CreatePlacementAsync(PlacementCreateDto dto)
    {
        // 1. Validation Logic: Check if foreign keys exist
        var assetExists = await _context.Assets.AnyAsync(a => a.ITEM_ID == dto.ItemId);
        if (!assetExists) 
            throw new KeyNotFoundException($"Asset with ID {dto.ItemId} not found.");

        var shelfExists = await _context.Shelf.AnyAsync(s => s.SHELF_ID == dto.ShelfId);
        if (!shelfExists) 
            throw new KeyNotFoundException($"Shelf with ID {dto.ShelfId} not found.");

        var rackExists = await _context.Rack.AnyAsync(r => r.RACK_ID == dto.RackId);
        if (!rackExists) 
            throw new KeyNotFoundException($"Rack with ID {dto.RackId} not found.");

        // 2. Business Logic: Mapping and Date Assignment
        var placement = new AssetPlacement
        {
            ITEM_ID = dto.ItemId,
            SHELF_ID = dto.ShelfId,
            RACK_ID = dto.RackId,
            PLACED_BY = dto.PlacedBy,
            PLACED_DATE = DateTime.UtcNow 
        };

        _context.Asset_Placement.Add(placement);
        await _context.SaveChangesAsync();

        return new PlacementReadDto(
            placement.ASSET_PLACEMENT_ID,
            placement.ITEM_ID,
            placement.SHELF_ID,
            placement.RACK_ID,
            placement.PLACED_DATE,
            placement.PLACED_BY
        );
    }

    public async Task<bool> UpdatePlacementAsync(int id, PlacementCreateDto dto)
    {
        var placement = await _context.Asset_Placement.FindAsync(id);
        if (placement == null) return false;

        placement.ITEM_ID = dto.ItemId;
        placement.SHELF_ID = dto.ShelfId;
        placement.RACK_ID = dto.RackId;
        placement.PLACED_BY = dto.PlacedBy;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePlacementAsync(int id)
    {
        var placement = await _context.Asset_Placement.FindAsync(id);
        if (placement == null) return false;

        _context.Asset_Placement.Remove(placement);
        await _context.SaveChangesAsync();
        return true;
    }
}