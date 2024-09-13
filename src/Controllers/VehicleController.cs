using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Dto;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;

    public VehicleController(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetVehicles()
    {
        var vehicles = _unityOfWork.VehicleRepository.GetVehicles();

        if (vehicles is null)
            return NotFound();

        var vehiclesDTO = _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);

        return Ok(vehiclesDTO);
    }

    [HttpGet("{vehicleId}", Name = "GetVehicle")]
    public IActionResult GetVehicle(int vehicleId)
    {
        var vehicle = _unityOfWork.VehicleRepository.GetVehicleById(vehicleId);

        if(vehicle is null)
            return NotFound();

        var vehicleDTO = _mapper.Map<VehicleDTO>(vehicle);

        return Ok(vehicleDTO);
    }

    [HttpPost]
    public IActionResult CreateVehicle(int carMakeId, int categoryId, VehicleDTO vehicleDTO)
    {
        if (vehicleDTO is null)
            return BadRequest();

        var vehicle = _mapper.Map<Vehicle>(vehicleDTO);

        vehicle.CarMakeId = carMakeId;
        vehicle.CategoryId = categoryId;

        var categoryExists = _unityOfWork.CategoryRepository.GetCategoryById(vehicle.CategoryId) != null;
        var carMakeExists = _unityOfWork.CarMakeRepository.GetCarMakeById(vehicle.CarMakeId) != null;

        if (!categoryExists)
            return BadRequest("Invalid Category ID.");

        if (!carMakeExists)
            return BadRequest("Invalid Car Make ID.");

        var createVehicle = _unityOfWork.VehicleRepository.CreateVehicle(vehicle);
        _unityOfWork.SaveChanges();

        var createVehicleDTO = _mapper.Map<VehicleDTO>(createVehicle);

        return new CreatedAtRouteResult("GetVehicle", new { vehicleId = createVehicleDTO.Id }, createVehicleDTO);
    }

    [HttpPut("{vehicleId}")]
    public IActionResult UpdateVehicle(int Id, VehicleDTO vehicleDTO)
    {
        if(Id != vehicleDTO.Id)
            return BadRequest("ID mismatch!");

        var vehicle = _mapper.Map<Vehicle>(vehicleDTO);

        var updateVehicle = _unityOfWork.VehicleRepository.UpdateVehicle(vehicle);
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
