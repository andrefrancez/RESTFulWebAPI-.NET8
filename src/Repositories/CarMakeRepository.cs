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

    public IEnumerable<CarMake> GetCarMakes()
    {
        return _context.CarMakes.ToList();
    }

    public IEnumerable<Vehicle> GetVehiclesByCarMake(int id)
    {
        return _context.Vehicles
                   .Where(v => v.CarMakeId == id)
                   .Include(v => v.CarMake)
                   .Include(v => v.Category)
                   .ToList();
    }

    public CarMake GetCarMakeById(int id)
    {
        return _context.CarMakes.FirstOrDefault(cm => cm.Id == id);
    }

    public bool CreateCarMake(CarMake carMake)
    {
        _context.CarMakes.Add(carMake);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateCarMake(CarMake carMake)
    {
        if (carMake is null)
            throw new ArgumentNullException(nameof(carMake));

        _context.Entry(carMake).State = EntityState.Modified;
        return _context.SaveChanges() > 0;
    }

    public bool DeleteCarMakeById(int id)
    {
        var carMake = _context.CarMakes.Find(id);

        if (carMake is null)
            throw new ArgumentException($"No car make found with Id {id}", nameof(id));

        _context.CarMakes.Remove(carMake);
        return _context.SaveChanges() > 0;
    }
}
