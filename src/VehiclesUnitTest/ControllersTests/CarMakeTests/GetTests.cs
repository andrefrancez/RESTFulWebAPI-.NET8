using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VehiclesAPI.Controllers;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesUnitTest.ControllersTests.CarMakeTests;

public class GetTests
{
    private readonly CarMakeController _controller;
    private readonly Mock<IUnityOfWork> _mockCarMake;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Fixture _fixture;

    public GetTests()
    {
        _mockCarMake = new Mock<IUnityOfWork>();
        _mockMapper = new Mock<IMapper>();
        _fixture = new Fixture();
        _fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _controller = new CarMakeController(_mockCarMake.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetCarMakesAsync_ReturnsOk()
    {
        //Arrange
        var carMakes = _fixture.CreateMany<CarMake>().ToList();

        _mockCarMake.Setup(x => x.CarMakeRepository.GetCarMakesAsync())
            .Returns(Task.FromResult<IEnumerable<CarMake>>(carMakes));

        //Act
        var result = await _controller.GetCarMakes();

        //Assert
        result.Should().BeOfType<OkObjectResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.GetCarMakesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCarMakesAsync_ReturnsNotFound()
    {
        //Arrange
        IEnumerable<CarMake> carMakes = Enumerable.Empty<CarMake>();

        _mockCarMake.Setup(x => x.CarMakeRepository.GetCarMakesAsync())
            .Returns(Task.FromResult(carMakes));

        //Act
        var result = await _controller.GetCarMakes();

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.GetCarMakesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCarMakeById_ReturnsOk()
    {
        //Arrange
        var carMake = _fixture.Create<CarMake>();

        _mockCarMake.Setup(x => x.CarMakeRepository.GetCarMakeByIdAsync(carMake.Id))
            .Returns(Task.FromResult(carMake));
        //Act
        var result = await _controller.GetCarMake(carMake.Id);

        //Assert
        result.Should().BeOfType<OkObjectResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.GetCarMakeByIdAsync(carMake.Id), Times.Once);
    }

    [Fact]
    public async Task GetCarMakeById_ReturnsNotFound()
    {
        //Arrange
        var carMake = _fixture.Create<CarMake>();

        _mockCarMake.Setup(x => x.CarMakeRepository.GetCarMakeByIdAsync(carMake.Id)).ReturnsAsync((CarMake)null);

        //Act
        var result = await _controller.GetCarMake(carMake.Id);

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.GetCarMakeByIdAsync(carMake.Id), Times.Once);
    }

    [Fact]
    public async Task GetVehiclesByCarMake_ReturnsOk()
    {
        //Arrange
        var carMake = _fixture.Create<CarMake>();
        var vehicles = _fixture.CreateMany<Vehicle>(5).ToList();

        vehicles[0].CarMakeId = carMake.Id;

        _mockCarMake.Setup(x => x.CarMakeRepository.GetVehiclesByCarMakeAsync(carMake.Id))
            .ReturnsAsync(vehicles);

        //Act
        var result = await _controller.GetVehiclesByCarMake(carMake.Id);

        //Assert
        result.Should().BeOfType<OkObjectResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.GetVehiclesByCarMakeAsync(carMake.Id), Times.Once);
    }

    [Fact]
    public async Task GetVehiclesByCarMake_ReturnsNotFound()
    {
        //Arrange
        var carMake = _fixture.Create<CarMake>();

        _mockCarMake.Setup(x => x.CarMakeRepository.GetVehiclesByCarMakeAsync(carMake.Id))
            .ReturnsAsync((IEnumerable<Vehicle>)null);

        //Act
        var result = await _controller.GetVehiclesByCarMake(carMake.Id);

        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.GetVehiclesByCarMakeAsync(carMake.Id), Times.Once);
    }
}