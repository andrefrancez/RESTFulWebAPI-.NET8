using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarMakeController : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork;

    public CarMakeController(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    [HttpGet]
    public IActionResult GetCarMakes()
    {
        var carMakes = _unityOfWork.CarMakeRepository.GetCarMakes();

        return Ok(carMakes);
    }

    [HttpGet("{Id}")]
    public IActionResult GetCarMake(int Id)
    {
        var carMake = _unityOfWork.CarMakeRepository.GetCarMakeById(Id);

        if (carMake is null)
            return NotFound();

        return Ok(carMake);
    }

    [HttpGet("vehicle/{Id}", Name = "GetCarMake")]
    public IActionResult GetVehiclesByCarMake(int Id)
    {
        var vehicles = _unityOfWork.CarMakeRepository.GetVehiclesByCarMake(Id);

        if (vehicles is null)
            return NotFound();

        return Ok(vehicles);
    }

    [HttpPost]
    public IActionResult CreateCarMake(CarMake carMake)
    {
        if (carMake is null)
            return BadRequest();

        _unityOfWork.CarMakeRepository.CreateCarMake(carMake);
        _unityOfWork.SaveChanges();

        return new CreatedAtRouteResult("GetCarMake", new { id = carMake.Id }, carMake);
    }

    [HttpPut("{Id}")]
    public IActionResult UpdateCarMake(int Id, CarMake carMake)
    {
        if (Id != carMake.Id)
            return BadRequest("ID mismatch!");

        _unityOfWork.CarMakeRepository.UpdateCarMake(carMake);
        _unityOfWork.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteCarMake(int Id)
    {
        _unityOfWork.CarMakeRepository.DeleteCarMakeById(Id);
        _unityOfWork.SaveChanges();

        return NoContent();
    }
}
