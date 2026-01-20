namespace ERP_BACKEND.interfaces;
using ERP_BACKEND.dtos;
public interface IStoreService
{
    Task<IEnumerable<readStoreDto>> GetAllStoresAsync();
    Task<readStoreDto?> GetStoreByIdAsync(int id);
    Task<readStoreDto> CreateStoreAsync(createStoreDto storeDto);
    Task<bool> UpdateStoreAsync(int id, createStoreDto storeDto);
    Task<bool> DeleteStoreAsync(int id);
}