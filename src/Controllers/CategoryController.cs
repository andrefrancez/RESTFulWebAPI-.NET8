using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork;

    public CategoryController(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _unityOfWork.CategoryRepository.GetCategories();

        if (categories is null)
            return NotFound();

        return Ok(categories);
    }

    [HttpGet("{categoryId}", Name="GetCategory")]
    public IActionResult GetCategory(int categoryId)
    {
        var category = _unityOfWork.CategoryRepository.GetCategoryById(categoryId);

        if(category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpGet("vehicle/{categoryId}")]
    public IActionResult GetVehicleByCategory(int categoryId)
    {
        var vehicles = _unityOfWork.CategoryRepository.GetVehiclesByCategory(categoryId);

        if(vehicles is null)
            return NotFound();

        return Ok(vehicles);
    }

    [HttpPost]
    public IActionResult CreateCategory(Category category)
    {
        if(category is null)
            return BadRequest();

        _unityOfWork.CategoryRepository.CreateCategory(category);
        _unityOfWork.SaveChanges();

        return new CreatedAtRouteResult("GetCategory", new { categoryId = category.Id }, category);
    }

    [HttpPut("{categoryId}")]
    public IActionResult UpdateCategory(int categoryId, Category category)
    {
        if(categoryId != category.Id)
            return BadRequest("ID mismatch!");

        _unityOfWork.CategoryRepository.UpdateCategory(category);
        _unityOfWork.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{categoryId}")]
    public IActionResult DeleteCategory(int categoryId)
    {
        _unityOfWork.CategoryRepository.DeleteCategoryById(categoryId);
        _unityOfWork.SaveChanges();

        return NoContent();
    }
}
