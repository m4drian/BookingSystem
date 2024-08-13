using BookingSystem.Application.Desks.Common;
using MediatR;

namespace BookingSystem.Application.Locations.Queries;

public record GetDesksInLocationQuery(
    string Name,
    string Role
) : IRequest<DesksResult>;