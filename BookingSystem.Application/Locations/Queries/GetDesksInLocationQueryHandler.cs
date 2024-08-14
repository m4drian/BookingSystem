using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Queries;
using BookingSystem.Application.Desks.Common;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.Authentication.Queries.Login;

public class GetDesksInLocationQueryHandler
    : IRequestHandler<GetDesksInLocationQuery, DesksResult>
{

    
    private readonly ILocationRepository _locationRepository;

    public GetDesksInLocationQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<DesksResult> Handle(GetDesksInLocationQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        GetDesksInLocationValidation(request);

        // check if location exists
        if(_locationRepository.GetLocationByName(request.Name) == null)
        {
            throw new NoLocationException();
        }

        // Get user role from claims (assuming the role claim is named "role")
        //var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

        List<Desk>? desks = _locationRepository.GetDesksInLocation(request.Name);

        return new DesksResult(
            desks);
    }

    private void GetDesksInLocationValidation(GetDesksInLocationQuery request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ValidationException("Location name is required");
        }
    }
}