using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    Task<Vehicle> GetVehicleByIdAsync(int id);
    Vehicle CreateVehicle(Vehicle vehicle);
    Vehicle UpdateVehicle(Vehicle vehicle);
    Vehicle DeleteVehicleById(int id);
}
