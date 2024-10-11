using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using VehiclesAPI.Dto;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("fixedwindow")]
public class CarMakeController(IUnityOfWork unityOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetCarMakes()
    {
        var carMakes = await _unityOfWork.CarMakeRepository.GetCarMakesAsync();

        var carMakesDTO = _mapper.Map<IEnumerable<CarMakeDTO>>(carMakes);

        return Ok(carMakesDTO);
    }

    [HttpGet("{Id}")]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetCarMake(int Id)
    {
        var carMake = await _unityOfWork.CarMakeRepository.GetCarMakeByIdAsync(Id);

        if (carMake is null)
            return NotFound();

        var carMakeDTO = _mapper.Map<CarMakeDTO>(carMake);

        return Ok(carMakeDTO);
    }

    [HttpGet("vehicle/{Id}", Name = "GetCarMake")]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetVehiclesByCarMake(int Id)
    {
        var vehicles = await _unityOfWork.CarMakeRepository.GetVehiclesByCarMakeAsync(Id);

        if (vehicles is null)
            return NotFound();

        var vehiclesDTO = _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);

        return Ok(vehiclesDTO);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateCarMake(CarMakeDTO carMakeDTO)
    {
        if (carMakeDTO is null)
            return BadRequest();

        var carMake = _mapper.Map<CarMake>(carMakeDTO);

        var CreateCarMake = _unityOfWork.CarMakeRepository.CreateCarMake(carMake);
        await _unityOfWork.SaveChangesAsync();

        var createCarMakeDTO = _mapper.Map<CarMakeDTO>(CreateCarMake);

        return new CreatedAtRouteResult("GetCarMake", new { id = createCarMakeDTO.Id }, createCarMakeDTO);
    }

    [HttpPut("{Id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateCarMake(int Id, CarMakeDTO carMakeDTO)
    {
        if (Id != carMakeDTO.Id)
            return BadRequest("ID mismatch!");

        var carMake = _mapper.Map<CarMake>(carMakeDTO);

        _unityOfWork.CarMakeRepository.UpdateCarMake(carMake);
        await _unityOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteCarMake(int Id)
    {
        _unityOfWork.CarMakeRepository.DeleteCarMakeById(Id);
        await _unityOfWork.SaveChangesAsync();

        return NoContent();
    }
}
