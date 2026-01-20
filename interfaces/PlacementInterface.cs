using ERP_BACKEND.dtos;
namespace ERP_BACKEND.interfaces;

public interface IPlacementService
{
    Task<IEnumerable<PlacementReadDto>> GetAllPlacementsAsync();
    Task<PlacementReadDto?> GetPlacementByIdAsync(int id);
    Task<PlacementReadDto> CreatePlacementAsync(PlacementCreateDto createDto);
    Task<bool> UpdatePlacementAsync(int id, PlacementCreateDto updateDto);
    Task<bool> DeletePlacementAsync(int id);
}