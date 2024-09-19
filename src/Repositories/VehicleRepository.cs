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

    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<Vehicle> GetVehicleByIdAsync(int id)
    {
        return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
    }

    public Vehicle CreateVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        return vehicle;
    }

    public Vehicle UpdateVehicle(Vehicle vehicle)
    {
        if(vehicle is null)
            throw new ArgumentNullException(nameof(vehicle));

        _context.Entry(vehicle).State = EntityState.Modified;
        return vehicle;
    }

    public Vehicle DeleteVehicleById(int id)
    {
        var vehicle = _context.Vehicles.Find(id);

        if (vehicle is null)
            throw new ArgumentException($"No vehicle found with Id {id}", nameof(id));

        _context.Vehicles.Remove(vehicle);
        return vehicle;
    }
}
