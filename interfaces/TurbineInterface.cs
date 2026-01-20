namespace ERP_BACKEND.interfaces;
using ERP_BACKEND.dtos;
public interface ITurbineService
{
    Task<IEnumerable<readTurbineDto>> GetAllTurbinesAsync();
    Task<readTurbineDto?> GetTurbineByIdAsync(int id);
    Task<readTurbineDto> CreateTurbineAsync(createTurbineDto turbineDto);
    Task<bool> UpdateTurbineAsync(int id, createTurbineDto turbineDto);
    Task<bool> DeleteTurbineAsync(int id);
}