using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;

namespace BookingSystem.Application.Desks.Commands;

public class UpdateDeskAdminCommandHandler
    : IRequestHandler<UpdateDeskAdminCommand, DeskResult>
{
    private readonly IDeskRepository _deskRepository;
    private readonly ILocationRepository _locationRepository;

    public UpdateDeskAdminCommandHandler(
        IDeskRepository deskRepository,
        ILocationRepository locationRepository)
    {
        _deskRepository = deskRepository;
        _locationRepository = locationRepository;
    }

    public async Task<DeskResult> Handle(UpdateDeskAdminCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new DuplicateLocationException();
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
}