using BookingSystem.Application.Desks.Common;
using MediatR;

namespace BookingSystem.Application.Desks.Commands;

public record UpdateDeskAdminCommand(
    string DeskId,
    string? UserEmail,
    bool? Available,
    DateTime? StartDate,
    DateTime? EndDate
) : IRequest<DeskResult>;