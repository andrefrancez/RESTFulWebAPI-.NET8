using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryRepository.GetCategories();

        if (categories is null)
            return NotFound();

        return Ok(categories);
    }

    [HttpGet("{categoryId}", Name="GetCategory")]
    public IActionResult GetCategory(int categoryId)
    {
        var category = _categoryRepository.GetCategoryById(categoryId);

        if(category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpGet("vehicle/{categoryId}")]
    public IActionResult GetVehicleByCategory(int categoryId)
    {
        var vehicles = _categoryRepository.GetVehiclesByCategory(categoryId);

        if(vehicles is null)
            return NotFound();

        return Ok(vehicles);
    }

    [HttpPost]
    public IActionResult CreateCategory(Category category)
    {
        if(category is null)
            return BadRequest();

        var categoryCreated = _categoryRepository.CreateCategory(category);

        return new CreatedAtRouteResult("GetCategory", new { categoryId = category.Id }, category);
    }

    [HttpPut("{categoryId}")]
    public IActionResult UpdateCategory(int categoryId, Category category)
    {
        if(categoryId != category.Id)
            return BadRequest();

        var categoryUpdated = _categoryRepository.UpdateCategory(category);

        return NoContent();
    }

    [HttpDelete("{categoryId}")]
    public IActionResult DeleteCategory(int categoryId)
    {
        var category = _categoryRepository.GetCategoryById(categoryId);

        if(category == null)
            return NotFound();

        var categoryDeleted = _categoryRepository.DeleteCategoryById(categoryId);

        return NoContent();
    }
}
