using BookingSystem.Application.Locations.Common;
using MediatR;

namespace BookingSystem.Application.Locations.Commands;

public record UpdateLocationCommand(
    string Name,
    string? ChangedName,
    string? Description
) : IRequest<LocationResult>;