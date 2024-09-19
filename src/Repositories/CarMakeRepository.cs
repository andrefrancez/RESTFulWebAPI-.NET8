using VehiclesAPI.Data;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace VehiclesAPI.Repositories;

public class CarMakeRepository : ICarMakeRepository
{
    private readonly DataContext _context;

    public CarMakeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CarMake>> GetCarMakesAsync()
    {
        return await _context.CarMakes.ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByCarMakeAsync(int id)
    {
        return await _context.Vehicles
                   .Where(v => v.CarMakeId == id)
                   .Include(v => v.CarMake)
                   .Include(v => v.Category)
                   .ToListAsync();
    }

    public async Task<CarMake> GetCarMakeByIdAsync(int id)
    {
        return await _context.CarMakes.FirstOrDefaultAsync(cm => cm.Id == id);
    }

    public CarMake CreateCarMake(CarMake carMake)
    {
        _context.CarMakes.Add(carMake);
        return carMake;
    }

    public CarMake UpdateCarMake(CarMake carMake)
    {
        if (carMake is null)
            throw new ArgumentNullException(nameof(carMake));

        _context.Entry(carMake).State = EntityState.Modified;
        return carMake;
    }

    public CarMake DeleteCarMakeById(int id)
    {
        var carMake = _context.CarMakes.Find(id);

        if (carMake is null)
            throw new ArgumentException($"No car make found with Id {id}", nameof(id));

        _context.CarMakes.Remove(carMake);
        return carMake;
    }
}
