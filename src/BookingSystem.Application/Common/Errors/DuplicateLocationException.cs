using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class DuplicateLocationException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Location already exists.";
}