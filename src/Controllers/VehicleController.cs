using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleController(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    [HttpGet]
    public IActionResult GetVehicles()
    {
        var vehicles = _vehicleRepository.GetVehicles();

        if (vehicles is null)
            return NotFound();

        return Ok(vehicles);
    }

    [HttpGet("{vehicleId}", Name = "GetVehicle")]
    public IActionResult GetVehicle(int vehicleId)
    {
        var vehicle = _vehicleRepository.GetVehicleById(vehicleId);

        if(vehicle is null)
            return NotFound();

        return Ok(vehicle);
    }

    [HttpPost]
    public IActionResult CreateVehicle(Vehicle vehicle)
    {
        if (vehicle is null)
            return BadRequest();

        var vehicleCreated = _vehicleRepository.CreateVehicle(vehicle);

        return new CreatedAtRouteResult("GetVehicle", new { vehicleId = vehicle.Id }, vehicle);
    }

    [HttpPut("{vehicleId}")]
    public IActionResult UpdateVehicle(int vehicleId, Vehicle vehicle)
    {
        if(vehicleId != vehicle.Id)
            return BadRequest();

        var vehicleUpdated = _vehicleRepository.UpdateVehicle(vehicle);

        return NoContent();
    }

    [HttpDelete("{vehicleId}")]
    public IActionResult DeleteVehicle(int vehicleId)
    {
        var vehicle = _vehicleRepository.GetVehicleById(vehicleId);

        if (vehicle is null)
            return NotFound();

        var vehicleDeleted = _vehicleRepository.DeleteVehicleById(vehicleId);

        return NoContent();
    }
}
