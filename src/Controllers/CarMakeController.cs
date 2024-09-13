using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Dto;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarMakeController : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;

    public CarMakeController(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCarMakes()
    {
        var carMakes = _unityOfWork.CarMakeRepository.GetCarMakes();

        var carMakesDTO = _mapper.Map<IEnumerable<CarMakeDTO>>(carMakes);

        return Ok(carMakesDTO);
    }

    [HttpGet("{Id}")]
    public IActionResult GetCarMake(int Id)
    {
        var carMake = _unityOfWork.CarMakeRepository.GetCarMakeById(Id);

        if (carMake is null)
            return NotFound();

        var carMakeDTO = _mapper.Map<CarMakeDTO>(carMake);

        return Ok(carMakeDTO);
    }

    [HttpGet("vehicle/{Id}", Name = "GetCarMake")]
    public IActionResult GetVehiclesByCarMake(int Id)
    {
        var vehicles = _unityOfWork.CarMakeRepository.GetVehiclesByCarMake(Id);

        if (vehicles is null)
            return NotFound();

        var vehiclesDTO = _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);

        return Ok(vehiclesDTO);
    }

    [HttpPost]
    public IActionResult CreateCarMake(CarMakeDTO carMakeDTO)
    {
        if (carMakeDTO is null)
            return BadRequest();

        var carMake = _mapper.Map<CarMake>(carMakeDTO);

        var CreateCarMake = _unityOfWork.CarMakeRepository.CreateCarMake(carMake);
        _unityOfWork.SaveChanges();

        var createCarMakeDTO = _mapper.Map<CarMakeDTO>(CreateCarMake);

        return new CreatedAtRouteResult("GetCarMake", new { id = createCarMakeDTO.Id }, createCarMakeDTO);
    }

    [HttpPut("{Id}")]
    public IActionResult UpdateCarMake(int Id, CarMakeDTO carMakeDTO)
    {
        if (Id != carMakeDTO.Id)
            return BadRequest("ID mismatch!");

        var carMake = _mapper.Map<CarMake>(carMakeDTO);

        var updateCarMake = _unityOfWork.CarMakeRepository.UpdateCarMake(carMake);
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
