using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarMakeController : ControllerBase
{
    private readonly ICarMakeRepository _carMakeRepository;

    public CarMakeController(ICarMakeRepository carMakeRepository)
    {
        _carMakeRepository = carMakeRepository;
    }

    [HttpGet]
    public IActionResult GetCarMakes()
    {
        throw new Exception("testestetset");
        var carMakes = _carMakeRepository.GetCarMakes();

        return Ok(carMakes);
    }

    [HttpGet("{Id}")]
    public IActionResult GetCarMake(int Id)
    {
        var carMake = _carMakeRepository.GetCarMakeById(Id);

        if (carMake is null)
            return NotFound();

        return Ok(carMake);
    }

    [HttpGet("vehicle/{Id}", Name = "GetCarMake")]
    public IActionResult GetVehiclesByCarMake(int Id)
    {
        var vehicles = _carMakeRepository.GetVehiclesByCarMake(Id);

        if (vehicles is null)
            return NotFound();

        return Ok(vehicles);
    }

    [HttpPost]
    public IActionResult CreateCarMake(CarMake carMake)
    {
        if (carMake is null)
            return BadRequest();

        _carMakeRepository.CreateCarMake(carMake);

        return new CreatedAtRouteResult("GetCarMake", new { id = carMake.Id }, carMake);
    }

    [HttpPut("{Id}")]
    public IActionResult UpdateCarMake(int Id, CarMake carMake)
    {
        if (Id != carMake.Id)
            return BadRequest();

        _carMakeRepository.UpdateCarMake(carMake);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteCarMake(int Id)
    {
        _carMakeRepository.DeleteCarMakeById(Id);

        return NoContent();
    }
}
