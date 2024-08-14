using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
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
        
        Desk? desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new NoDeskException();
        }

        UpdateDeskValidation(request);

        // as per project requirments - admin cannot change dates or emails, 
        // he can only remove them

        // if desk changed to available, then clear employees booking
        // leave values as they are if no value specified
        desk.Available = request.Available ?? desk.Available;

        // this is the only case where Available can be null
        if (request.Available.HasValue && request.Available.Value)
        {
            desk.UserEmail = request.Available == true ? null : desk.UserEmail;
            desk.ReservationStartDate = request.Available == true ? null : 
                desk.ReservationStartDate;
            desk.ReservationEndDate = request.Available == true ? null : 
                desk.ReservationEndDate;
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

        if (!string.IsNullOrEmpty(request.UserEmail) && !ValidateHelper.IsValidEmail(request.UserEmail))
        {
            throw new ValidationException("Bad email format");
        }
    }
}