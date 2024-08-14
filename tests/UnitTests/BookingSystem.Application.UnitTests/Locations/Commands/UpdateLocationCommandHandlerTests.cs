using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Locations.Commands;
using BookingSystem.Domain.Entities;
using Moq;

namespace BookingSystem.Application.UnitTests.Desks.Commands;

public class UpdateLocationCommandHandlerTests
{
    [Fact]
    public async Task Handle_EmptyName_ThrowsValidationException()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        var mockDesksRepository = new Mock<IDeskRepository>();
        var command = new UpdateLocationCommand ( "" , "" , "");
        var handler = new UpdateLocationCommandHandler(mockLocationRepository.Object, mockDesksRepository.Object);

        // Act + Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));
        mockLocationRepository.VerifyNoOtherCalls();
        mockDesksRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_NonExistingLocation_ThrowsNoLocationException()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        var mockDesksRepository = new Mock<IDeskRepository>();
        mockLocationRepository.Setup(x => x.GetLocationByName(It.IsAny<string>())).Returns((Location)null);
        var command = new UpdateLocationCommand ( "NonExistingName" , "" , "" );
        var handler = new UpdateLocationCommandHandler(mockLocationRepository.Object, mockDesksRepository.Object);

        // Act + Assert
        await Assert.ThrowsAsync<NoLocationException>(async () => await handler.Handle(command, CancellationToken.None));
        mockLocationRepository.Verify(x => x.GetLocationByName(command.Name), Times.Once);
        mockDesksRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ValidUpdateWithChangedName_UpdatesLocationAndDesks()
    {
    // Arrange
    var existingLocation = new Location { Id = Guid.NewGuid(), Name = "Old Name", Description = "Some Description" };
    var mockLocationRepository = new Mock<ILocationRepository>();
    mockLocationRepository.Setup(x => x.GetLocationByName(It.IsAny<string>())).Returns(existingLocation);
    var mockDesksRepository = new Mock<IDeskRepository>();
    var command = new UpdateLocationCommand ( "Old Name", "New Name", "Updated Description" );
    var handler = new UpdateLocationCommandHandler(mockLocationRepository.Object, mockDesksRepository.Object);

    // Act
    var result = await handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(command.ChangedName, result.Location.Name); // Expect New Name here
    Assert.Equal(command.Description, result.Location.Description);
    mockLocationRepository.Verify(x => x.Update(existingLocation, command.ChangedName, command.Description), Times.Once);
    mockDesksRepository.Verify(x => x.UpdateAllDeskLocations(existingLocation), Times.Once);
    }
    
}