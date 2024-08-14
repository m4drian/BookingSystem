using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Locations.Commands;

public class DeleteLocationCommandHandler
    : IRequestHandler<DeleteLocationCommand, LocationResult>
{
    
    private readonly ILocationRepository _locationRepository;

    public DeleteLocationCommandHandler(
        ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<LocationResult> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        DeleteLocationValidation(request);
        
        var location = _locationRepository.GetLocationByName(request.Name);

        // check if location exists
        if(location == null)
        {
            throw new NoLocationException();
        }

        // check if location has desks
        if(location.Desks.Any())
        {
            throw new DesksInLocationException();
        }

        var locationToDelete = new Location
        {
            Name = request.Name,
            Description = "",
        };

        _locationRepository.Delete(locationToDelete);

        return new LocationResult(
            locationToDelete);
    }

    public void DeleteLocationValidation(DeleteLocationCommand request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ValidationException("Location name is required");
        }
    }
}