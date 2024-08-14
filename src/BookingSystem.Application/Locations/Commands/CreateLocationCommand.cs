using BookingSystem.Application.Locations.Common;
using MediatR;

namespace BookingSystem.Application.Locations.Commands;

public record CreateLocationCommand(
    string Name,
    string? Description
) : IRequest<LocationResult>;