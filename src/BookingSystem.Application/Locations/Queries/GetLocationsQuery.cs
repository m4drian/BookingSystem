using BookingSystem.Application.Locations.Common;
using MediatR;

namespace BookingSystem.Application.Locations.Queries;

public record GetLocationsQuery(
) : IRequest<LocationsResult>;