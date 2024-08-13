using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;
using MediatR;
using BookingSystem.Application.Locations.Common;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;

namespace BookingSystem.Application.Desks.Commands;

public class DeleteDeskCommandHandler
    : IRequestHandler<DeleteDeskCommand, DeskResult>
{
    private readonly IDeskRepository _deskRepository;

    public DeleteDeskCommandHandler(
        IDeskRepository deskRepository)
    {
        _deskRepository = deskRepository;
    }

    public async Task<DeskResult> Handle(DeleteDeskCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var desk = _deskRepository.GetDeskById(new Guid(request.DeskId));

        // check if desk exists
        if(desk == null)
        {
            throw new DuplicateLocationException();
        }

        _deskRepository.DeleteDesk(new Guid(request.DeskId));

        return new DeskResult(
            desk);
    }
}