using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Queries;
using BookingSystem.Application.Desks.Common;
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