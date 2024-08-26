using Microsoft.EntityFrameworkCore;
using VehiclesAPI.Data;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public IEnumerable<Vehicle> GetVehiclesByCategory(int id)
    {
        return _context.Vehicles
                   .Where(v => v.CategoryId == id)
                   .Include(v => v.Category)
                   .Include(v => v.CarMake)
                   .ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.Id == id);
    }

    public Category CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        return category;
    }

    public Category UpdateCategory(Category category)
    {
        if(category is null)
            throw new ArgumentNullException(nameof(category));

        _context.Entry(category).State = EntityState.Modified;
        return category;
    }

    public Category DeleteCategoryById(int id)
    {
        var category = _context.Categories.Find(id);

        if (category is null)
            throw new ArgumentException($"No category found with Id {id}", nameof(id));

        _context.Categories.Remove(category);
        return category;
    }
}
