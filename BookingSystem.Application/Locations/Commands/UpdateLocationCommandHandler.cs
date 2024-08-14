using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Locations.Commands;

public class UpdateLocationCommandHandler
    : IRequestHandler<UpdateLocationCommand, LocationResult>
{
    
    private readonly ILocationRepository _locationRepository;
    private readonly IDeskRepository _desksRepository;

    public UpdateLocationCommandHandler(
        ILocationRepository locationRepository,
        IDeskRepository deskRepository)
    {
        _locationRepository = locationRepository;
        _desksRepository = deskRepository;
    }

    public async Task<LocationResult> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        UpdateLocationValidation(request);
        
        // check if location exists
        if(_locationRepository.GetLocationByName(request.Name) == null)
        {
            throw new NoLocationException();
        }

        var location = new Location
        {
            Name = request.ChangedName ?? request.Name,
            Description = request.Description,
        };

        _locationRepository.Update(location);

        if(!string.IsNullOrEmpty(request.ChangedName))
        {
            _desksRepository.UpdateAllDeskLocations(location);
        }

        return new LocationResult(
            location);
    }

    private void UpdateLocationValidation(UpdateLocationCommand request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ValidationException("Location name is required");
        }
    }
}