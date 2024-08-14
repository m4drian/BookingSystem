using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class WrongDeskSelectedException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Cannot change this booking.";
}