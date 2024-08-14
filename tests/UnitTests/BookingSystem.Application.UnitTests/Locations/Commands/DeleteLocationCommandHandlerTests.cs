using System.ComponentModel.DataAnnotations;
using System.Reflection;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Locations.Commands;
using BookingSystem.Domain.Entities;
using Moq;

namespace BookingSystem.Application.UnitTests.Desks.Commands;

public class DeleteLocationCommandHandlerTests
{ 
    [Fact]
    public async Task Handle_LocationNotFound_ShouldThrowNoLocationException()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        mockLocationRepository.Setup(repo => repo.GetLocationByName("NonExistentLocation")).Returns((Location)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        var handler = new DeleteLocationCommandHandler(mockLocationRepository.Object);

        // Act & Assert
        async Task ActAndAssert() => await handler.Handle(new DeleteLocationCommand ( "NonExistentLocation" ), CancellationToken.None);
        await Assert.ThrowsAsync<NoLocationException>(ActAndAssert);

        mockLocationRepository.Verify(repo => repo.GetLocationByName("NonExistentLocation"), Times.Once);
        mockLocationRepository.Verify(repo => repo.Delete(It.IsAny<Location>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DesksInLocation_ShouldThrowDesksInLocationException()
    {
        var mockLocationRepository = new Mock<ILocationRepository>();
        var mockLocation = new Location { Name = "TestLocation" };
        mockLocationRepository.Setup(repo => repo.GetLocationByName("TestLocation")).Returns(mockLocation);

        var handler = new DeleteLocationCommandHandler(mockLocationRepository.Object);

        var deskWithLocation = new Desk { Location = mockLocation };
        var locationWithDesks = new List<Desk>() { deskWithLocation };

        mockLocation.Desks = locationWithDesks;

        async Task ActAndAssert() => await handler.Handle(new DeleteLocationCommand ( "TestLocation" ), CancellationToken.None);
        await Assert.ThrowsAsync<DesksInLocationException>(ActAndAssert);

        mockLocationRepository.Verify(repo => repo.GetLocationByName("TestLocation"), Times.Once);
        mockLocationRepository.Verify(repo => repo.Delete(It.IsAny<Location>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidDeletion_ShouldDeleteLocationSuccessfully()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        var mockLocation = new Location { Name = "TestLocation" };
        mockLocationRepository.Setup(repo => repo.GetLocationByName("TestLocation")).Returns(mockLocation);
        mockLocationRepository.Setup(repo => repo.Delete(It.IsAny<Location>())).Verifiable();

        var handler = new DeleteLocationCommandHandler(mockLocationRepository.Object);

        // Act
        var result = await handler.Handle(new DeleteLocationCommand ( "TestLocation" ), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockLocation.Name, result.Location.Name);
        mockLocationRepository.Verify(repo => repo.GetLocationByName("TestLocation"), Times.Once);
        mockLocationRepository.Verify(repo => repo.Delete(It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public void Handle_NameIsEmpty_ShouldThrowValidationException()
    {
        // Arrange
        var handler = new DeleteLocationCommandHandler(Mock.Of<ILocationRepository>()); // Mock the repository

        // Act & Assert
        Assert.Throws<ValidationException>(() => handler.DeleteLocationValidation(new DeleteLocationCommand ( "" )));
    }
}