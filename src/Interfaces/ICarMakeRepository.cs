using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces;

public interface ICarMakeRepository
{
    Task<IEnumerable<CarMake>> GetCarMakesAsync();
    Task<IEnumerable<Vehicle>> GetVehiclesByCarMakeAsync(int id);
    Task<CarMake> GetCarMakeByIdAsync(int id);
    CarMake CreateCarMake(CarMake carMake);
    CarMake UpdateCarMake(CarMake carMake);
    CarMake DeleteCarMakeById(int id);
}
