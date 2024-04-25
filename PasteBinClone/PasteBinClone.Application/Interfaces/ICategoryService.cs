using PasteBinClone.Application.Dto;

namespace PasteBinClone.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryByID(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task<bool> CreateCategory(CategoryDto category);
        Task<bool> DeleteCategory(int id);
        Task<bool> UpdateCategory(CategoryDto categoryDto);
    }
}
