using System.Net;

namespace BookingSystem.Application.Authentication.Common.Errors;

public class DeskOccupiedException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Desk cannot be removed because it has an active reservation.";
}