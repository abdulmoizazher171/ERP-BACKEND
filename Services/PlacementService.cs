using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;
using AutoMapper;
using ERP_BACKEND.mappers;
using AutoMapper.QueryableExtensions;
namespace ERP_BACKEND.services;

public class PlacementService : IPlacementService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PlacementService(AppDbContext context , IMapper  mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlacementMapper>> GetAllPlacementsAsync()
    {
        // return await _context.Asset_Placement
        //     .Select(p => new PlacementReadDto(
        //         p.PLACEMENT_ID,
        //         p.ITEM_ID,
        //         p.SHELF_ID,
        //         p.RACK_ID,

        //         p.PLACED_DATE,
        //         p.PLACED_BY,
        //         p.WITHDRAWAL_DATE,
        //         p.WITHDRAWN_BY,
        //         p.LOCATION


        //     )).ToListAsync();


             List<PlacementMapper> assetPlacements = new List<PlacementMapper>();
        await _context.Asset_Placement.ToListAsync();
        assetPlacements = await _context.Asset_Placement 
        .Include(p=> p.Asset).Include(p=>p.Rack).Include(p=>p.Shelf).Include(p=>p.Asset.Turbine).Include(p=>p.Asset.Category)
        .ProjectTo<PlacementMapper>(_mapper.ConfigurationProvider)
        .ToListAsync();
        return assetPlacements;
    }

    public async Task<PlacementReadDto?> GetPlacementByIdAsync(int id)
    {
        var p = await _context.Asset_Placement.FindAsync(id);
        if (p == null) return null;

       
        return new PlacementReadDto(
            p.PLACEMENT_ID,
            p.ITEM_ID,
            p.SHELF_ID,
            p.RACK_ID,
            p.PLACED_DATE,
            p.PLACED_BY,
            p.WITHDRAWAL_DATE,
            p.WITHDRAWN_BY ?? "",
            p.LOCATION,
            p.QUANTITY

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
            placement.PLACEMENT_ID,
            placement.ITEM_ID,
            placement.SHELF_ID,
            placement.RACK_ID,
            placement.PLACED_DATE,
            placement.PLACED_BY,
            placement.WITHDRAWAL_DATE,
            placement.WITHDRAWN_BY,
            placement.LOCATION,
            placement.QUANTITY
        );
    }

    public async Task<bool> UpdatePlacementAsync(int id, PlacementModifyDto dto)
    {
        var placement = await _context.Asset_Placement.FindAsync(id);
        if (placement == null) return false;

        placement.ITEM_ID = dto.ItemId;
        placement.SHELF_ID = dto.ShelfId;
        placement.RACK_ID = dto.RackId;
        placement.PLACED_BY = dto.PlacedBy;
        placement.WITHDRAWAL_DATE = dto.WithdrawalDate;
        placement.WITHDRAWN_BY =  dto.WithdrawalBy;
        placement.LOCATION = dto.Location;



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