using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<IEnumerable<Vehicle>> GetVehiclesByCategoryAsync(int id);
    Task<Category> GetCategoryByIdAsync(int id);
    Category CreateCategory(Category category);
    Category UpdateCategory(Category category);
    Category DeleteCategoryById(int id);
}
