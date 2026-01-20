namespace ERP_BACKEND.interfaces;
using ERP_BACKEND.dtos;
public interface IRackService
{
    Task<IEnumerable<readRackDto>> GetAllRacksAsync();
    Task<readRackDto?> GetRackByIdAsync(int id);
    Task<readRackDto> CreateRackAsync(createRackDto rackDto);
    Task<bool> UpdateRackAsync(int id, createRackDto rackDto);
    Task<bool> DeleteRackAsync(int id);
}