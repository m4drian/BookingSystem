using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Locations.Commands;
using BookingSystem.Domain.Entities;
using Moq;

namespace BookingSystem.Application.UnitTests.Desks.Commands;

public class DeleteLocationCommandHandlerTests
{ 
    [Fact]
    public async Task Handle_EmptyName_ShouldThrowValidationException()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        var command = new CreateLocationCommand ( "" , "" );
        var handler = new CreateLocationCommandHandler(mockLocationRepository.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));
        mockLocationRepository.Verify(x => x.GetLocationByName(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DuplicateLocation_ShouldThrowDuplicateLocationException()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        mockLocationRepository.Setup(x => x.GetLocationByName(It.IsAny<string>())).Returns(new Location());
        var command = new CreateLocationCommand ( "Valid Name" , "Valid Description" );
        var handler = new CreateLocationCommandHandler(mockLocationRepository.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<DuplicateLocationException>(async () => await handler.Handle(command, CancellationToken.None));
        mockLocationRepository.Verify(x => x.GetLocationByName(command.Name), Times.Once);
    }

    [Fact]
    public async Task Handle_ValidLocation_ShouldReturnLocationResult()
    {
        // Arrange
        var mockLocationRepository = new Mock<ILocationRepository>();
        mockLocationRepository.Setup(x => x.GetLocationByName(It.IsAny<string>())).Returns(Mock.Of<Location>());
        var command = new CreateLocationCommand ( "Valid Name", "Some Description" );
        var handler = new CreateLocationCommandHandler(mockLocationRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Location.Name);
        Assert.Equal(command.Description, result.Location.Description);
        mockLocationRepository.Verify(x => x.GetLocationByName(command.Name), Times.Once);
    }
}