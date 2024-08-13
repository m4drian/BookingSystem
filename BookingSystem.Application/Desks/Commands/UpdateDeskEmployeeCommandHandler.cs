using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using MediatR;
using BookingSystem.Application.Desks.Common;
using BookingSystem.Application.Authentication.Common.Interfaces.Services;

namespace BookingSystem.Application.Desks.Commands;

public class UpdateDeskEmployeeCommandHandler
    : IRequestHandler<UpdateDeskEmployeeCommand, DeskResult>
{
    private readonly IDeskRepository _deskRepository;
    private readonly IDateTimeProvider _clock;

    public UpdateDeskEmployeeCommandHandler(
        IDeskRepository deskRepository,
        IDateTimeProvider clock)
    {
        _deskRepository = deskRepository;
        _clock = clock;
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
        if (request.Available == true && desk.ReservationStartDate > _clock.UtcNow.AddDays(1))
        {
            throw new DuplicateLocationException();
        }

        desk.UserEmail = request.Available == false ? request.UserEmail : null;
        desk.Available = request.Available;
        desk.ReservationStartDate = request.Available == false ? request.StartDate : null;

        // cannot book desk for more than a week
        if (request.EndDate > request.StartDate.AddDays(6))
        {
            throw new DuplicateLocationException();
        }

        if (request.Available)
        {
            // in case reservation is cancelled, dates are reset
            desk.ReservationEndDate = null;
        }
        else
        {
            // if no end date specified, book for a day
            desk.ReservationEndDate = request.EndDate ?? request.StartDate.AddDays(1);
        }

        _deskRepository.ReserveDeskEmployee(desk);

        return new DeskResult(
            desk);
    }
}