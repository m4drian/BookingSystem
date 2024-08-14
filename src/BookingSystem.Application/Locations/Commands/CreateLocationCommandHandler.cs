using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Locations.Commands;

public class CreateLocationCommandHandler
    : IRequestHandler<CreateLocationCommand, LocationResult>
{
    
    private readonly ILocationRepository _locationRepository;

    public CreateLocationCommandHandler(
        ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<LocationResult> Handle(
        CreateLocationCommand request, 
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        CreateLocationValidation(request);
        
        // check if location exists
        if(_locationRepository.GetLocationByName(request.Name) != null)
        {
            throw new DuplicateLocationException();
        }

        // create location, generate ID and persist to DB
        var location = new Location
        {
            Name = request.Name,
            Description = request.Description,
        };

        _locationRepository.Add(location);

        return new LocationResult(
            location);
    }

    private void CreateLocationValidation(CreateLocationCommand request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ValidationException("Location name is required");
        }
    }
}