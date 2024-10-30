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

public class PutTests
{
    private readonly CarMakeController _controller;
    private readonly Mock<IUnityOfWork> _mockCarMake;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Fixture _fixture;

    public PutTests()
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
    public async Task UpdateCarMake_ReturnsNoContent()
    {
        //Arrange
        var carMakeDTO = _fixture.Create<CarMakeDTO>();
        var carMake = _fixture.Build<CarMake>()
            .With(cm => cm.Id, carMakeDTO.Id)
            .Create();

        _mockMapper.Setup(x => x.Map<CarMake>(carMakeDTO)).Returns(carMake);
        _mockCarMake.Setup(x => x.CarMakeRepository.UpdateCarMake(carMake));
        _mockCarMake.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.UpdateCarMake(carMake.Id, carMakeDTO);

        //Assert
        result.Should().BeOfType<NoContentResult>();

        _mockMapper
            .Verify(x => x.Map<CarMake>(carMakeDTO), Times.Once);
        _mockCarMake
            .Verify(x => x.CarMakeRepository.UpdateCarMake(carMake), Times.Once);
        _mockCarMake
            .Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateCarMake_ReturnsBadRequest()
    {
        //Arrange
        var carMakeDTO = _fixture.Create<CarMakeDTO>();
        var carMake = _fixture.Create<CarMake>();

        //Act
        var result = await _controller.UpdateCarMake(carMake.Id, carMakeDTO);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("ID mismatch!");
    }
}
