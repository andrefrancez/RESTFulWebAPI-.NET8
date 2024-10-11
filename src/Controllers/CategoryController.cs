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
public class CategoryController(IUnityOfWork unityOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;


    [HttpGet]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _unityOfWork.CategoryRepository.GetCategoriesAsync();

        if (categories is null)
            return NotFound();

        var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

        return Ok(categoriesDTO);
    }

    [HttpGet("{categoryId}", Name="GetCategory")]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetCategory(int categoryId)
    {
        var category = await _unityOfWork.CategoryRepository.GetCategoryByIdAsync(categoryId);

        if(category is null)
            return NotFound();

        var categoryDTO = _mapper.Map<IEnumerable<CategoryDTO>>(category);

        return Ok(categoryDTO);
    }

    [HttpGet("vehicle/{categoryId}")]
    [Authorize(Policy = "UserOnly")]
    public async Task<IActionResult> GetVehicleByCategory(int categoryId)
    {
        var vehicles = await _unityOfWork.CategoryRepository.GetVehiclesByCategoryAsync(categoryId);

        if(vehicles is null)
            return NotFound();

        var vehiclesDTO = _mapper.Map<IEnumerable<Vehicle>>(vehicles);

        return Ok(vehiclesDTO);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateCategory(CategoryDTO categoryDTO)
    {
        if(categoryDTO is null)
            return BadRequest();

        var category = _mapper.Map<Category>(categoryDTO);

        var createCategory = _unityOfWork.CategoryRepository.CreateCategory(category);
        await _unityOfWork.SaveChangesAsync();

        var createCategoryDTO = _mapper.Map<CategoryDTO>(createCategory);

        return new CreatedAtRouteResult("GetCategory", new { categoryId = createCategoryDTO.Id }, createCategoryDTO);
    }

    [HttpPut("{categoryId}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateCategory(int categoryId, CategoryDTO categoryDTO)
    {
        if(categoryId != categoryDTO.Id)
            return BadRequest("ID mismatch!");

        var category = _mapper.Map<Category>(categoryDTO);

        _unityOfWork.CategoryRepository.UpdateCategory(category);
        await _unityOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{categoryId}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        _unityOfWork.CategoryRepository.DeleteCategoryById(categoryId);
        await _unityOfWork.SaveChangesAsync();

        return NoContent();
    }
}
