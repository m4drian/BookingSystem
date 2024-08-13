using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Authentication.Common.Interfaces.Services;
using BookingSystem.Application.Common.Helpers;

namespace BookingSystem.Application.Desks.Commands;

public class UpdateDeskAdminCommandHandler
    : IRequestHandler<UpdateDeskAdminCommand, DeskResult>
{
    private readonly IDeskRepository _deskRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IDateTimeProvider _clock;

    public UpdateDeskAdminCommandHandler(
        IDeskRepository deskRepository,
        ILocationRepository locationRepository,
        IDateTimeProvider clock)
    {
        _deskRepository = deskRepository;
        _locationRepository = locationRepository;
        _clock = clock;
    }

    public async Task<DeskResult> Handle(UpdateDeskAdminCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        UpdateDeskValidation(request);
        
        var desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new NoDeskException();
        }

        // if desk changed to available, then clear employees booking
        // leave values as they are if no value specified
        desk.Available = request.Available ?? desk.Available;

        // this is the only case where Available can be null
        if (request.Available.HasValue && request.Available.Value)
        {
            desk.UserEmail = request.Available == true ? "" : 
                (request.UserEmail ?? desk.UserEmail);
            desk.ReservationStartDate = request.Available == true ? null : 
                (request.StartDate ?? desk.ReservationStartDate);
            desk.ReservationEndDate = request.Available == true ? null : 
                (request.EndDate ?? desk.ReservationEndDate);
        }
        else
        {
            desk.UserEmail = request.UserEmail ?? desk.UserEmail;
            desk.ReservationStartDate = request.StartDate ?? desk.ReservationStartDate;
            desk.ReservationEndDate = request.EndDate ?? desk.ReservationEndDate;
        }

        _deskRepository.UpdateDeskAdmin(desk);

        return new DeskResult(
            desk);
    }

    private void UpdateDeskValidation(UpdateDeskAdminCommand request)
    {

        if (string.IsNullOrEmpty(request.DeskId))
        {
            throw new ValidationException("Desk Id is required");
        }

        if (string.IsNullOrEmpty(request.UserEmail) || !ValidateHelper.IsValidEmail(request.UserEmail))
        {
            throw new ValidationException("Bad email format");
        }
    
        if (request.StartDate == null || request.StartDate < _clock.UtcNow)
        {
            throw new ValidationException("Start date must be in the future");
        }

        if (request.EndDate.HasValue && request.EndDate < request.StartDate)
        {
            throw new ValidationException("End date must be greater than or equal to start date");
        }
    }
}