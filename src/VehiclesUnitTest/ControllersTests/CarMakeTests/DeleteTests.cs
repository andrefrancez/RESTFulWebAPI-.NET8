using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VehiclesAPI.Controllers;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesUnitTest.ControllersTests.CarMakeTests;

public class DeleteTests
{
    private readonly CarMakeController _controller;
    private readonly Mock<IUnityOfWork> _mockCarMake;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Fixture _fixture;

    public DeleteTests()
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
    public async Task DeleteCarMake_ReturnsNoContent()
    {
        // Arrange
        var carMake = _fixture.Create<CarMake>();

        _mockCarMake.Setup(x => x.CarMakeRepository.GetCarMakeByIdAsync(carMake.Id)).ReturnsAsync(carMake);
        _mockCarMake.Setup(x => x.CarMakeRepository.DeleteCarMakeById(carMake.Id)).Returns(carMake);
        // Act
        var result = await _controller.DeleteCarMake(carMake.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.DeleteCarMakeById(carMake.Id), Times.Once);
    }

    [Fact]
    public async Task DeleteCarMake_ReturnsNotFound()
    {
        // Arrange
        var carMake = _fixture.Create<CarMake>();

        _mockCarMake.Setup(x => x.CarMakeRepository.GetCarMakeByIdAsync(carMake.Id)).ReturnsAsync((CarMake)null);

        // Act
        var result = await _controller.DeleteCarMake(carMake.Id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();

        _mockCarMake
            .Verify(x => x.CarMakeRepository.GetCarMakeByIdAsync(carMake.Id), Times.Once);
        _mockCarMake
            .Verify(x => x.CarMakeRepository.DeleteCarMakeById(carMake.Id), Times.Never);
    }
}
