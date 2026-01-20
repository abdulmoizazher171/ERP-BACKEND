namespace ERP_BACKEND.interfaces;
using ERP_BACKEND.dtos;
public interface ITurbineService
{
    Task<IEnumerable<TurbineDto>> GetAllTurbinesAsync();
    Task<TurbineDto?> GetTurbineByIdAsync(int id);
    Task<TurbineDto> CreateTurbineAsync(TurbineDto turbineDto);
    Task<bool> UpdateTurbineAsync(int id, TurbineDto turbineDto);
    Task<bool> DeleteTurbineAsync(int id);
}