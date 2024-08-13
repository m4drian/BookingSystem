using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;

namespace BookingSystem.Application.Locations.Commands;

public class CreateLocationCommandHandler
    : IRequestHandler<CreateLocationCommand, LocationResult>
{
    
    private readonly ILocationRepository _locationRepository;

    public CreateLocationCommandHandler(
        ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<LocationResult> Handle(
        CreateLocationCommand request, 
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        // check if location exists
        if(_locationRepository.GetLocationByName(request.Name) != null)
        {
            throw new NoLocationException();
        }

        // create location, generate ID and persist to DB
        var location = new Location
        {
            Name = request.Name,
            Description = request.Description,
        };

        _locationRepository.Add(location);

        return new LocationResult(
            location);
    }
}