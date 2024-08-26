using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface IVehicleRepository
{
    IEnumerable<Vehicle> GetVehicles();
    Vehicle GetVehicleById(int id);
    Vehicle CreateVehicle(Vehicle vehicle);
    Vehicle UpdateVehicle(Vehicle vehicle);
    Vehicle DeleteVehicleById(int id);
}
