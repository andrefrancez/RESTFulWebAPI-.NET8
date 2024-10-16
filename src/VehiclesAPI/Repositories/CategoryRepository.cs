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

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByCategoryAsync(int id)
    {
        return await _context.Vehicles
                   .Where(v => v.CategoryId == id)
                   .Include(v => v.Category)
                   .Include(v => v.CarMake)
                   .ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
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
