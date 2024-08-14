using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class DesksInLocationException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Location cannot be deleted because it has assigned desks.";
}