using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;

namespace BookingSystem.Application.Locations.Commands;

public class UpdateLocationCommandHandler
    : IRequestHandler<UpdateLocationCommand, LocationResult>
{
    
    private readonly ILocationRepository _locationRepository;

    public UpdateLocationCommandHandler(
        ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<LocationResult> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        // check if location exists
        if(_locationRepository.GetLocationByName(request.Name) == null)
        {
            throw new DuplicateLocationException();
        }

        var location = new Location
        {
            Name = request.ChangedName ?? request.Name,
            Description = request.Description,
        };

        _locationRepository.Update(location);

        return new LocationResult(
            location);
    }
}