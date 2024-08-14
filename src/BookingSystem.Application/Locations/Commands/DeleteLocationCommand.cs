using BookingSystem.Application.Locations.Common;
using MediatR;

namespace BookingSystem.Application.Locations.Commands;

public record DeleteLocationCommand(
    string Name
) : IRequest<LocationResult>;