using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;

namespace ERP_BACKEND.services;

public class RackService : IRackService
{
    private readonly AppDbContext _context;

    public RackService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RackDto>> GetAllRacksAsync()
    {
        return await _context.Racks
            .Select(r => new RackDto(r.RackId, r.RackNumber))
            .ToListAsync();
    }

    public async Task<RackDto?> GetRackByIdAsync(int id)
    {
        var rack = await _context.Racks.FindAsync(id);
        return rack == null ? null : new RackDto(rack.RackId, rack.RackNumber);
    }

    public async Task<RackDto> CreateRackAsync(RackDto rackDto)
    {
        var rack = new Rack
        {
            RackNumber = rackDto.RackNumber
        };

        _context.Racks.Add(rack);
        await _context.SaveChangesAsync();

        return new RackDto(rack.RackId, rack.RackNumber);
    }

    public async Task<bool> UpdateRackAsync(int id, RackDto rackDto)
    {
        var rack = await _context.Racks.FindAsync(id);
        if (rack == null) return false;

        rack.RackNumber = rackDto.RackNumber;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRackAsync(int id)
    {
        var rack = await _context.Racks.FindAsync(id);
        if (rack == null) return false;

        _context.Racks.Remove(rack);
        await _context.SaveChangesAsync();
        return true;
    }
}