using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using MediatR;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;

namespace BookingSystem.Application.Desks.Queries;

public class GetDeskReservationQueryHandler
    : IRequestHandler<GetDeskReservationQuery, ReservationResult>
{
    private readonly IDeskRepository _deskRepository;

    public GetDeskReservationQueryHandler(
        IDeskRepository deskRepository)
    {
        _deskRepository = deskRepository;
    }

    public async Task<ReservationResult> Handle(GetDeskReservationQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new DuplicateLocationException();
        }

        return new ReservationResult(
            desk.Available);
    }
}