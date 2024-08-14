using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using MediatR;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;
using System.ComponentModel.DataAnnotations;

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

        GetDeskReservationValidation(request);
        
        var desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new NoDeskException();
        }

        return new ReservationResult(
            desk.Available);
    }

    private void GetDeskReservationValidation(GetDeskReservationQuery request)
    {

        if (string.IsNullOrEmpty(request.DeskId))
        {
            throw new ValidationException("Desk Id is required");
        }
    }
}