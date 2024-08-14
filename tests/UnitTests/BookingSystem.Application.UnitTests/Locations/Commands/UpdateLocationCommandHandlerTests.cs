using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Locations.Commands;
using BookingSystem.Domain.Entities;
using Moq;
using NSubstitute;

namespace BookingSystem.Application.UnitTests.Desks.Commands;

public class UpdateLocationCommandHandlerTests
{

    [Fact]
    public async Task Handle_ValidLocation_ShouldCompleteUpdatingLocationAndDesksSuccessfully()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        var mockDeskRepository = new Mock<IDeskRepository>();

        var existingLocation = new Location { Name = "ExistingLocation" };

        mockLocationRepository.Setup(repo => repo.GetLocationByName(It.IsAny<string>()))
            .Returns(existingLocation);

        var handler = new UpdateLocationCommandHandler(mockLocationRepository.Object, mockDeskRepository.Object);

        var command = new UpdateLocationCommand
        (
            "ExistingLocation",
            "NewLocation",
            "Updated Description"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockLocationRepository.Verify(repo => repo.GetLocationByName("ExistingLocation"), Times.Once);
#pragma warning disable CS8601 // Possible null reference assignment.
        existingLocation.Name = command.ChangedName;
#pragma warning restore CS8601 // Possible null reference assignment.
        mockLocationRepository.Verify(repo => repo.Update(existingLocation, "NewLocation", "Updated Description"), Times.Once);
        mockDeskRepository.Verify(repo => repo.UpdateAllDeskLocations(existingLocation), Times.Once);

        Assert.NotNull(result);
        Assert.Equal("NewLocation", result.Location.Name);
    }

    [Fact]
    public async Task Handle_LocationNotFound_ShouldThrowNoLocationException()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        var mockDeskRepository = new Mock<IDeskRepository>();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        mockLocationRepository.Setup(repo => repo.GetLocationByName("NonexistentLocation")).Returns((Location)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        var handler = new UpdateLocationCommandHandler(mockLocationRepository.Object, mockDeskRepository.Object);

        var command = new UpdateLocationCommand ( "NonexistentLocation" , "", "" );

        // Act & Assert
        await Assert.ThrowsAsync<NoLocationException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public void Handle_NameIsEmpty_ShouldThrowValidationException()
    {
        // Arrange
        var handler = new UpdateLocationCommandHandler(Mock.Of<ILocationRepository>(), Mock.Of<IDeskRepository>()); // Mock both repositories

        // Act & Assert
        Assert.Throws<ValidationException>(() => handler.UpdateLocationValidation(new UpdateLocationCommand ( "", "","" )));
    }
    
}