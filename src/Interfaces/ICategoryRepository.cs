using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    IEnumerable<Vehicle> GetVehiclesByCategory(int id);
    Category GetCategoryById(int id);
    bool CreateCategory(Category category);
    bool UpdateCategory(Category category);
    bool DeleteCategoryById(int id);
}
