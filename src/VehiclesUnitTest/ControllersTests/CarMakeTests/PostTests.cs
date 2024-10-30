using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VehiclesAPI.Controllers;
using VehiclesAPI.Dto;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesUnitTest.ControllersTests.CarMakeTests;

public class PostTests
{
    private readonly CarMakeController _controller;
    private readonly Mock<IUnityOfWork> _mockCarMake;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Fixture _fixture;

    public PostTests()
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
    public async Task CreateCarMake_ReturnsCreated()
    {
        //Arrange
        var carMakeDTO = _fixture.Create<CarMakeDTO>();
        var carMake = _fixture.Build<CarMake>()
            .With(c => c.Id, carMakeDTO.Id)
            .Create();

        _mockMapper.Setup(x => x.Map<CarMake>(carMakeDTO)).Returns(carMake);
        _mockMapper.Setup(x => x.Map<CarMakeDTO>(carMake)).Returns(carMakeDTO);
        _mockCarMake.Setup(x => x.CarMakeRepository.CreateCarMake(carMake)).Returns(carMake);
        _mockCarMake.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.CreateCarMake(carMakeDTO);

        //Assert
        var createdResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;

        createdResult.RouteName.Should().Be("GetCarMake");
        createdResult.RouteValues["id"].Should().Be(carMakeDTO.Id);
        createdResult.Value.Should().BeEquivalentTo(carMakeDTO);

        _mockMapper
            .Verify(x => x.Map<CarMake>(carMakeDTO), Times.Once);
        _mockMapper
            .Verify(x => x.Map<CarMakeDTO>(carMake), Times.Once);
        _mockCarMake
            .Verify(x => x.CarMakeRepository.CreateCarMake(carMake), Times.Once);
        _mockCarMake
            .Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateCarMake_ReturnsBadRequest()
    {
        //Arrange
        CarMakeDTO carMakeDTO = null;

        //Act
        var result = await _controller.CreateCarMake(carMakeDTO);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Data cannot be null.");
    }
}
