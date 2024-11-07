using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VehiclesAPI.Controllers;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesUnitTest.ControllersTests.CategoryTests;

public class GetTests
{
    private readonly CategoryController _controller;
    private readonly Mock<IUnityOfWork> _mockCategory; 
    private readonly Mock<IMapper> _mockMapper;
    private readonly Fixture _fixture;
    public GetTests()
    {
        _mockCategory = new Mock<IUnityOfWork>();
        _mockMapper = new Mock<IMapper>();
        _fixture = new Fixture();
        _fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _controller = new CategoryController(_mockCategory.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetCategories_ReturnsOkResult()
    {
        //Arrange
        var categories = _fixture.CreateMany<Category>();

        _mockCategory.Setup(x => x.CategoryRepository.GetCategoriesAsync())
            .ReturnsAsync(categories);
        //Act
        var result = await _controller.GetCategories();

        //Assert
        result.Should().BeOfType<OkObjectResult>();

        _mockCategory
            .Verify(x => x.CategoryRepository.GetCategoriesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCategories_ReturnsNotFound()
    {
        //Arrange
        IEnumerable<Category> category = Enumerable.Empty<Category>();

        _mockCategory.Setup(x => x.CategoryRepository.GetCategoriesAsync())
            .Returns(Task.FromResult(category));

        //Act
        var result = await _controller.GetCategories();

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();

        _mockCategory
            .Verify(x => x.CategoryRepository.GetCategoriesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCategoryById_ReturnsOkResult()
    {
        //Arrange
        var category = _fixture.Create<Category>();

        _mockCategory.Setup(x => x.CategoryRepository.GetCategoryByIdAsync(category.Id))
            .Returns(Task.FromResult(category));

        //Act
        var result = await _controller.GetCategory(category.Id);

        //Assert
        result.Should().BeOfType<OkObjectResult>();

        _mockCategory
            .Verify(x => x.CategoryRepository.GetCategoryByIdAsync(category.Id), Times.Once);
    }

    [Fact]
    public async Task GetCategoryById_ReturnsNotFound()
    {
        //Arrange
        var category = _fixture.Create<Category>();

        _mockCategory.Setup(x => x.CategoryRepository.GetCategoryByIdAsync(category.Id))
            .ReturnsAsync((Category)null);

        //Act
        var result = await _controller.GetCategory(category.Id);

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();

        _mockCategory
            .Verify(x => x.CategoryRepository.GetCategoryByIdAsync(category.Id), Times.Once);
    }

    [Fact]
    public async Task GetVehiclesByCategory_ReturnsOk()
    {
        //Arrange
        var category = _fixture.Create<Category>();
        var vehicles = _fixture.CreateMany<Vehicle>(5).ToList();

        vehicles[0].CategoryId = category.Id;

        _mockCategory.Setup(x => x.CategoryRepository.GetVehiclesByCategoryAsync(category.Id))
            .ReturnsAsync(vehicles);

        //Act
        var result = await _controller.GetVehicleByCategory(category.Id);

        //Assert
        result.Should().BeOfType<OkObjectResult>();

        _mockCategory
            .Verify(x => x.CategoryRepository.GetVehiclesByCategoryAsync(category.Id), Times.Once);
    }

    [Fact]
    public async Task GetVehiclesByCategory_ReturnsNotFound()
    {
        //Arrange
        var category = _fixture.Create<Category>();

        _mockCategory.Setup(x => x.CategoryRepository.GetVehiclesByCategoryAsync(category.Id))
            .ReturnsAsync((IEnumerable<Vehicle>)null);

        //Act
        var result = await _controller.GetVehicleByCategory(category.Id);

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();

        _mockCategory
            .Verify(x => x.CategoryRepository.GetVehiclesByCategoryAsync(category.Id), Times.Once);
    }
}