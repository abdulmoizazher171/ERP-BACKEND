namespace ERP_BACKEND.interfaces;
using ERP_BACKEND.dtos;
public interface IStoreService
{
    Task<IEnumerable<StoreDto>> GetAllStoresAsync();
    Task<StoreDto?> GetStoreByIdAsync(int id);
    Task<StoreDto> CreateStoreAsync(StoreDto storeDto);
    Task<bool> UpdateStoreAsync(int id, StoreDto storeDto);
    Task<bool> DeleteStoreAsync(int id);
}