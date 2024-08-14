using BookingSystem.Application.Desks.Common;
using MediatR;

namespace BookingSystem.Application.Desks.Queries;

public record GetDeskReservationQuery(
    string DeskId
) : IRequest<ReservationResult>;