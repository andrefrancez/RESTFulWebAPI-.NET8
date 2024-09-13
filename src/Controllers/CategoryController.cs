using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Dto;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;

    public CategoryController(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _unityOfWork.CategoryRepository.GetCategories();

        if (categories is null)
            return NotFound();

        var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

        return Ok(categoriesDTO);
    }

    [HttpGet("{categoryId}", Name="GetCategory")]
    public IActionResult GetCategory(int categoryId)
    {
        var category = _unityOfWork.CategoryRepository.GetCategoryById(categoryId);

        if(category is null)
            return NotFound();

        var categoryDTO = _mapper.Map<IEnumerable<CategoryDTO>>(category);

        return Ok(categoryDTO);
    }

    [HttpGet("vehicle/{categoryId}")]
    public IActionResult GetVehicleByCategory(int categoryId)
    {
        var vehicles = _unityOfWork.CategoryRepository.GetVehiclesByCategory(categoryId);

        if(vehicles is null)
            return NotFound();

        var vehiclesDTO = _mapper.Map<IEnumerable<Vehicle>>(vehicles);

        return Ok(vehiclesDTO);
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryDTO categoryDTO)
    {
        if(categoryDTO is null)
            return BadRequest();

        var category = _mapper.Map<Category>(categoryDTO);

        var createCategory = _unityOfWork.CategoryRepository.CreateCategory(category);
        _unityOfWork.SaveChanges();

        var createCategoryDTO = _mapper.Map<CategoryDTO>(createCategory);

        return new CreatedAtRouteResult("GetCategory", new { categoryId = createCategoryDTO.Id }, createCategoryDTO);
    }

    [HttpPut("{categoryId}")]
    public IActionResult UpdateCategory(int Id, CategoryDTO categoryDTO)
    {
        if(Id != categoryDTO.Id)
            return BadRequest("ID mismatch!");

        var category = _mapper.Map<Category>(categoryDTO);

        var updateCategory = _unityOfWork.CategoryRepository.UpdateCategory(category);
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
