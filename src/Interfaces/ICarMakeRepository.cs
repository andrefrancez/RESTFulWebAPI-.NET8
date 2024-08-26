using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface ICarMakeRepository
{
    IEnumerable<CarMake> GetCarMakes();
    IEnumerable<Vehicle> GetVehiclesByCarMake(int id);
    CarMake GetCarMakeById(int id);
    CarMake CreateCarMake(CarMake carMake);
    CarMake UpdateCarMake(CarMake carMake);
    CarMake DeleteCarMakeById(int id);
}
