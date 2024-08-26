using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    IEnumerable<Vehicle> GetVehiclesByCategory(int id);
    Category GetCategoryById(int id);
    Category CreateCategory(Category category);
    Category UpdateCategory(Category category);
    Category DeleteCategoryById(int id);
}
