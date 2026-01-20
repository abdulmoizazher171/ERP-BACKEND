using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using Microsoft.EntityFrameworkCore;
using ERP_BACKEND.data;

namespace ERP_BACKEND.services;

public class TurbineService : ITurbineService
{
    private readonly AppDbContext _context;

    public TurbineService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<readTurbineDto>> GetAllTurbinesAsync()
    {
        return await _context.Turbines
            .Select(t => new readTurbineDto(t.TURBINE_ID, t.SYSTEM_NUMBER))
            .ToListAsync();
    }

    public async Task<readTurbineDto?> GetTurbineByIdAsync(int id)
    {
        var turbine = await _context.Turbines.FindAsync(id);
        if (turbine == null) return null;

        return new readTurbineDto(turbine.TURBINE_ID, turbine.SYSTEM_NUMBER);
    }

    public async Task<readTurbineDto> CreateTurbineAsync(createTurbineDto turbineDto)
    {
        var turbine = new Turbine
        {
            SYSTEM_NUMBER = turbineDto.SystemNumber
        };

        _context.Turbines.Add(turbine);
        await _context.SaveChangesAsync();

        return new readTurbineDto(turbine.TURBINE_ID, turbine.SYSTEM_NUMBER);
    }

    public async Task<bool> UpdateTurbineAsync(int id, createTurbineDto turbineDto)
    {
        var turbine = await _context.Turbines.FindAsync(id);
        if (turbine == null) return false;

        turbine.SYSTEM_NUMBER = turbineDto.SystemNumber;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTurbineAsync(int id)
    {
        var turbine = await _context.Turbines.FindAsync(id);
        if (turbine == null) return false;

        _context.Turbines.Remove(turbine);
        await _context.SaveChangesAsync();
        return true;
    }
}