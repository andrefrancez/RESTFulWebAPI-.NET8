using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Dto;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController(IUnityOfWork unityOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;


    [HttpGet]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetVehicles()
    {
        var vehicles = await _unityOfWork.VehicleRepository.GetVehiclesAsync();

        if (vehicles is null)
            return NotFound();

        var vehiclesDTO = _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);

        return Ok(vehiclesDTO);
    }

    [HttpGet("{vehicleId}", Name = "GetVehicle")]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetVehicle(int vehicleId)
    {
        var vehicle = await _unityOfWork.VehicleRepository.GetVehicleByIdAsync(vehicleId);

        if(vehicle is null)
            return NotFound();

        var vehicleDTO = _mapper.Map<VehicleDTO>(vehicle);

        return Ok(vehicleDTO);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateVehicle(int carMakeId, int categoryId, VehicleDTO vehicleDTO)
    {
        if (vehicleDTO is null)
            return BadRequest();

        var vehicle = _mapper.Map<Vehicle>(vehicleDTO);

        vehicle.CarMakeId = carMakeId;
        vehicle.CategoryId = categoryId;

        var categoryExists = await _unityOfWork.CategoryRepository.GetCategoryByIdAsync(vehicle.CategoryId) != null;
        var carMakeExists = await _unityOfWork.CarMakeRepository.GetCarMakeByIdAsync(vehicle.CarMakeId) != null;

        if (!categoryExists)
            return BadRequest("Invalid Category ID.");

        if (!carMakeExists)
            return BadRequest("Invalid Car Make ID.");

        var createVehicle = _unityOfWork.VehicleRepository.CreateVehicle(vehicle);
        await _unityOfWork.SaveChangesAsync();

        var createVehicleDTO = _mapper.Map<VehicleDTO>(createVehicle);

        return new CreatedAtRouteResult("GetVehicle", new { vehicleId = createVehicleDTO.Id }, createVehicleDTO);
    }

    [HttpPut("{vehicleId}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateVehicle(int vehicleId, VehicleDTO vehicleDTO)
    {
        if(vehicleId != vehicleDTO.Id)
            return BadRequest("ID mismatch!");

        var vehicle = _mapper.Map<Vehicle>(vehicleDTO);

        _unityOfWork.VehicleRepository.UpdateVehicle(vehicle);
        await _unityOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{vehicleId}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteVehicle(int vehicleId)
    {
        _unityOfWork.VehicleRepository.DeleteVehicleById(vehicleId);
        await _unityOfWork.SaveChangesAsync();

        return NoContent();
    }
}
