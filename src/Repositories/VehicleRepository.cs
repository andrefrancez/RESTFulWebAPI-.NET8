using Microsoft.EntityFrameworkCore;
using VehiclesAPI.Data;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly DataContext _context;

    public VehicleRepository(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Vehicle> GetVehicles()
    {
        return _context.Vehicles.ToList();
    }

    public Vehicle GetVehicleById(int id)
    {
        return _context.Vehicles.FirstOrDefault(v => v.Id == id);
    }

    public bool CreateVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateVehicle(Vehicle vehicle)
    {
        if(vehicle is null)
            throw new ArgumentNullException(nameof(vehicle));

        _context.Entry(vehicle).State = EntityState.Modified;
        return _context.SaveChanges() > 0;
    }

    public bool DeleteVehicleById(int id)
    {
        var vehicle = _context.Vehicles.Find(id);

        if (vehicle is null)
            throw new ArgumentException($"No vehicle found with Id {id}", nameof(id));

        _context.Vehicles.Remove(vehicle);
        return _context.SaveChanges() > 0;
    }
}
