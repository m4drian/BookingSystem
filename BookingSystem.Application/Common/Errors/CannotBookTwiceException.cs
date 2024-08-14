using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class CannotBookTwiceException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Cannot have multiple bookings / change existing ones.";
}