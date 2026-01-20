namespace ERP_BACKEND.interfaces;
using ERP_BACKEND.dtos;
public interface IRackService
{
    Task<IEnumerable<RackDto>> GetAllRacksAsync();
    Task<RackDto?> GetRackByIdAsync(int id);
    Task<RackDto> CreateRackAsync(RackDto rackDto);
    Task<bool> UpdateRackAsync(int id, RackDto rackDto);
    Task<bool> DeleteRackAsync(int id);
}