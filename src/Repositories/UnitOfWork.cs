using VehiclesAPI.Data;
using VehiclesAPI.Interfaces;

namespace VehiclesAPI.Repositories;

public class UnitOfWork : IUnityOfWork
{
    private ICarMakeRepository _carMakeRepository;

    private ICategoryRepository _categoryRepository;

    private IVehicleRepository _vehicleRepository;

    public DataContext _context { get; set; }

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public ICarMakeRepository CarMakeRepository
    {
        get
        {
            return _carMakeRepository = _carMakeRepository ?? new CarMakeRepository(_context);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        }
    }

    public IVehicleRepository VehicleRepository
    {
        get
        {
            return _vehicleRepository = _vehicleRepository ?? new VehicleRepository(_context);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
