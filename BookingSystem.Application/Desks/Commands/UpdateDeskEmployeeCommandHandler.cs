using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using MediatR;
using BookingSystem.Application.Desks.Common;
using BookingSystem.Application.Authentication.Common.Interfaces.Services;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Common.Helpers;

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

        UpdateDeskValidation(request);
        
        var desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new NoDeskException();
        }

        // employee cannot cancel booking less than 24h before reservation
        if (request.Available == true && desk.ReservationStartDate > _clock.UtcNow.AddDays(1))
        {
            throw new CancellationTooEarlyException();
        }

        desk.UserEmail = request.Available == false ? request.UserEmail : null;
        desk.Available = request.Available;
        desk.ReservationStartDate = request.Available == false ? request.StartDate : null;

        // cannot book desk for more than a week
        if (request.EndDate > request.StartDate.AddDays(6))
        {
            throw new BookingTooLongException();
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

    private void UpdateDeskValidation(UpdateDeskEmployeeCommand request)
    {

        if (string.IsNullOrEmpty(request.DeskId))
        {
            throw new ValidationException("Desk Id is required");
        }

        if (string.IsNullOrEmpty(request.UserEmail) || !ValidateHelper.IsValidEmail(request.UserEmail))
        {
            throw new ValidationException("Bad email format");
        }
    
        if (request.StartDate < _clock.UtcNow)
        {
            throw new ValidationException("Start date must be in the future");
        }

        if (request.EndDate.HasValue && request.EndDate < request.StartDate)
        {
            throw new ValidationException("End date must be greater than or equal to start date");
        }
    }
}