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

    public async Task<IEnumerable<readRackDto>> GetAllRacksAsync()
    {
        return await _context.Rack
            .Select(r => new readRackDto(r.RACK_ID, r.RACK_NUMBER))
            .ToListAsync();
    }

    public async Task<readRackDto?> GetRackByIdAsync(int id)
    {
        var rack = await _context.Rack.FindAsync(id);
        return rack == null ? null : new readRackDto(rack.RACK_ID, rack.RACK_NUMBER);
    }

    public async Task<readRackDto> CreateRackAsync(createRackDto rackDto)
    {
        var rack = new Rack
        {
            RACK_NUMBER = rackDto.RackNumber
        };

        _context.Rack.Add(rack);
        await _context.SaveChangesAsync();

        return new readRackDto(rack.RACK_ID, rack.RACK_NUMBER);
    }

    public async Task<bool> UpdateRackAsync(int id, createRackDto rackDto)
    {
        var rack = await _context.Rack.FindAsync(id);
        if (rack == null) return false;

        rack.RACK_NUMBER = rackDto.RackNumber;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRackAsync(int id)
    {
        var rack = await _context.Rack.FindAsync(id);
        if (rack == null) return false;

        _context.Rack.Remove(rack);
        await _context.SaveChangesAsync();
        return true;
    }
}