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

        desk.UserEmail = request.UserEmail ?? desk.UserEmail;
        desk.Available = request.Available ?? desk.Available;
        desk.ReservationStartDate = request.StartDate ?? desk.ReservationStartDate;
        desk.ReservationEndDate = request.EndDate ?? desk.ReservationEndDate;

        _deskRepository.UpdateDeskAdmin(desk);

        return new DeskResult(
            desk);
    }
}