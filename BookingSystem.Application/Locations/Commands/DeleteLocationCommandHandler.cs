using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;

namespace BookingSystem.Application.Locations.Commands;

public class DeleteLocationCommandHandler
    : IRequestHandler<DeleteLocationCommand, LocationResult>
{
    
    private readonly ILocationRepository _locationRepository;

    public DeleteLocationCommandHandler(
        ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<LocationResult> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var location = _locationRepository.GetLocationByName(request.Name);

        // check if location exists
        if(location == null)
        {
            throw new DuplicateLocationException();
        }

        // check if location has desks
        if(!location?.Desks?.Any() ?? false)
        {
            throw new DuplicateLocationException();
        }

        var locationToDelete = new Location
        {
            Name = request.Name,
            Description = "",
        };

        _locationRepository.Delete(locationToDelete);

        return new LocationResult(
            locationToDelete);
    }
}