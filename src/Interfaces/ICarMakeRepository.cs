using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface ICarMakeRepository
{
    IEnumerable<CarMake> GetCarMakes();
    IEnumerable<Vehicle> GetVehiclesByCarMake(int id);
    CarMake GetCarMakeById(int id);
    bool CreateCarMake(CarMake carMake);
    bool UpdateCarMake(CarMake carMake);
    bool DeleteCarMakeById(int id);
}
