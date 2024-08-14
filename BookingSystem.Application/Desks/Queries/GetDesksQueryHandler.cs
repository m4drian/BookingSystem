using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using MediatR;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;
using BookingSystem.Application.Desks.Query;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Desks.Queries;

public class GetDesksQueryHandler
    : IRequestHandler<GetDesksQuery, DesksResult>
{
    private readonly IDeskRepository _deskRepository;

    public GetDesksQueryHandler(
        IDeskRepository deskRepository)
    {
        _deskRepository = deskRepository;
    }

    public async Task<DesksResult> Handle(GetDesksQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        List<Desk>? desks = _deskRepository.GetAllDesks();

        return new DesksResult(
            desks);
    }
}