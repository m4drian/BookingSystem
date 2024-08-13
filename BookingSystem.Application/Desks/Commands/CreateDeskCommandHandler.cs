using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Desks.Common;

namespace BookingSystem.Application.Desks.Commands;

public class CreateDeskCommandHandler
    : IRequestHandler<CreateDeskCommand, DeskResult>
{
    private readonly IDeskRepository _deskRepository;
    private readonly ILocationRepository _locationRepository;

    public CreateDeskCommandHandler(
        IDeskRepository deskRepository,
        ILocationRepository locationRepository)
    {
        _deskRepository = deskRepository;
        _locationRepository = locationRepository;
    }

    public async Task<DeskResult> Handle(CreateDeskCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var location = _locationRepository.GetLocationByName(request.LocationName);

        // check if location exists
        if(location == null)
        {
            throw new DuplicateLocationException();
        }

        // create location
        var desk = new Desk
        {
            LocationId = location.Id,
            Location = location,
            UserEmail = request.UserEmail,
            Available = request.Available,
            ReservationStartDate = request.StartDate,
            ReservationEndDate = request.EndDate,
        };

        _deskRepository.Add(desk, location.Id);

        return new DeskResult(
            desk);
    }
}