using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Desks.Common;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Common.Helpers;
using BookingSystem.Application.Authentication.Common.Interfaces.Services;

namespace BookingSystem.Application.Desks.Commands;

public class CreateDeskCommandHandler
    : IRequestHandler<CreateDeskCommand, DeskResult>
{
    private readonly IDeskRepository _deskRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IDateTimeProvider _clock;

    public CreateDeskCommandHandler(
        IDeskRepository deskRepository,
        ILocationRepository locationRepository,
        IDateTimeProvider clock)
    {
        _deskRepository = deskRepository;
        _locationRepository = locationRepository;
        _clock = clock;
    }

    public async Task<DeskResult> Handle(CreateDeskCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        CreateDeskValidation(request);
        
        var location = _locationRepository.GetLocationByName(request.LocationName);

        // check if location exists
        if(location == null)
        {
            throw new NoLocationException();
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

        _deskRepository.Add(desk, location);

        return new DeskResult(
            desk);
    }

    public void CreateDeskValidation(CreateDeskCommand request)
    {
        if (string.IsNullOrEmpty(request.LocationName))
        {
            throw new ValidationException("Location name is required");
        }

        if (!string.IsNullOrEmpty(request.UserEmail) && !ValidateHelper.IsValidEmail(request.UserEmail))
        {
            throw new ValidationException("Bad email format");
        }
    
        if (request.StartDate != null && request.StartDate < _clock.UtcNow)
        {
            throw new ValidationException("Start date must be in the future");
        }

        if (request.EndDate.HasValue && request.EndDate < request.StartDate)
        {
            throw new ValidationException("End date must be greater than or equal to start date");
        }
    }
}