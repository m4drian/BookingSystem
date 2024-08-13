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

        desk.UserEmail = request.UserEmail;
        // validate if can cancel
        desk.Available = request.Available;
        desk.ReservationStartDate = request.StartDate;
        // end date validation
        desk.ReservationEndDate = request.EndDate ?? desk.ReservationEndDate;

        _deskRepository.ReserveDeskEmployee(desk);

        return new DeskResult(
            desk);
    }
}