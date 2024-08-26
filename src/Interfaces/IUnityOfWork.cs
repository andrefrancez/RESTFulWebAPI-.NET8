namespace VehiclesAPI.Interfaces;

public interface IUnityOfWork
{
    ICarMakeRepository CarMakeRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    IVehicleRepository VehicleRepository { get; }

    void SaveChanges();
}
