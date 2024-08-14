using BookingSystem.Application.Desks.Common;
using MediatR;

namespace BookingSystem.Application.Desks.Commands;

public record DeleteDeskCommand(
    string DeskId
) : IRequest<DeskResult>;