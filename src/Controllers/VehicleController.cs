using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork;

    public VehicleController(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    [HttpGet]
    public IActionResult GetVehicles()
    {
        var vehicles = _unityOfWork.VehicleRepository.GetVehicles();

        if (vehicles is null)
            return NotFound();

        return Ok(vehicles);
    }

    [HttpGet("{vehicleId}", Name = "GetVehicle")]
    public IActionResult GetVehicle(int vehicleId)
    {
        var vehicle = _unityOfWork.VehicleRepository.GetVehicleById(vehicleId);

        if(vehicle is null)
            return NotFound();

        return Ok(vehicle);
    }

    [HttpPost]
    public IActionResult CreateVehicle(Vehicle vehicle)
    {
        if (vehicle is null)
            return BadRequest();

        var categoryExists = _unityOfWork.CategoryRepository.GetCategoryById(vehicle.CategoryId) != null;
        var carMakeExists = _unityOfWork.CarMakeRepository.GetCarMakeById(vehicle.CarMakeId) != null;

        if (!categoryExists)
            return BadRequest("Invalid Category ID.");

        if (!carMakeExists)
            return BadRequest("Invalid Car Make ID.");

        _unityOfWork.VehicleRepository.CreateVehicle(vehicle);
        _unityOfWork.SaveChanges();

        return new CreatedAtRouteResult("GetVehicle", new { vehicleId = vehicle.Id }, vehicle);
    }

    [HttpPut("{vehicleId}")]
    public IActionResult UpdateVehicle(int vehicleId, Vehicle vehicle)
    {
        if(vehicleId != vehicle.Id)
            return BadRequest("ID mismatch!");

        _unityOfWork.VehicleRepository.UpdateVehicle(vehicle);
        _unityOfWork.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{vehicleId}")]
    public IActionResult DeleteVehicle(int vehicleId)
    {
        _unityOfWork.VehicleRepository.DeleteVehicleById(vehicleId);
        _unityOfWork.SaveChanges();

        return NoContent();
    }
}
