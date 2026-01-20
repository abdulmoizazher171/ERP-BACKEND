using  ERP_BACKEND.dtos;
public interface ICategoryService
{
    Task<IEnumerable<readCategoryDto>> GetAllCategoriesAsync();
    Task<readCategoryDto?> GetCategoryByIdAsync(int id);
    Task<readCategoryDto> CreateCategoryAsync(createCategoryDto categoryDto);
    Task<bool> UpdateCategoryAsync(int id, createCategoryDto categoryDto);
    Task<bool> DeleteCategoryAsync(int id);
}