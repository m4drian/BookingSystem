using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Queries;
using BookingSystem.Application.Locations.Common;

namespace BookingSystem.Application.Authentication.Queries.Login;

public class GetLocationsQueryHandler
    : IRequestHandler<GetLocationsQuery, LocationsResult>
{

    
    private readonly ILocationRepository _locationRepository;

    public GetLocationsQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<LocationsResult> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        List<Location>? locations = _locationRepository.GetAllLocations();

        return new LocationsResult(
            locations);
    }
}