using BookingSystem.Application.Desks.Common;
using MediatR;

namespace BookingSystem.Application.Desks.Query;

public record GetDesksQuery(
) : IRequest<DesksResult>;