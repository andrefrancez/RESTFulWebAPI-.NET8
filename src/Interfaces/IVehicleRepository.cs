using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface IVehicleRepository
{
    IEnumerable<Vehicle> GetVehicles();
    Vehicle GetVehicleById(int id);
    bool CreateVehicle(Vehicle vehicle);
    bool UpdateVehicle(Vehicle vehicle);
    bool DeleteVehicleById(int id);
}
