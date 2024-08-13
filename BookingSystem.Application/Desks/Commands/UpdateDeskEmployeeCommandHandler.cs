using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;

namespace BookingSystem.Application.Desks.Commands;

public class UpdateDeskEmployeeCommandHandler
    : IRequestHandler<UpdateDeskEmployeeCommand, DeskResult>
{
    private readonly IDeskRepository _deskRepository;

    public UpdateDeskEmployeeCommandHandler(
        IDeskRepository deskRepository)
    {
        _deskRepository = deskRepository;
    }

    public async Task<DeskResult> Handle(UpdateDeskEmployeeCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new DuplicateLocationException();
        }

        // employee cannot cancel booking less than 24h before reservation
        if (request.Available == true && desk.ReservationStartDate > DateTime.UtcNow.AddDays(1))
        {
            throw new DuplicateLocationException();
        }

        desk.UserEmail = request.Available == false ? request.UserEmail : null;
        desk.Available = request.Available;
        desk.ReservationStartDate = request.Available == false ? request.StartDate : null;

        // if no end date specified, book for a day
        // cannot book desk for more than a week
        if (request.EndDate > request.StartDate.AddDays(6))
        {
            throw new DuplicateLocationException();
        }

        desk.ReservationEndDate = request.Available == false ? (request.EndDate ?? request.StartDate.AddDays(1)) : null;

        _deskRepository.ReserveDeskEmployee(desk);

        return new DeskResult(
            desk);
    }
}