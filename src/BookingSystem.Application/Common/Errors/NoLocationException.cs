using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class NoLocationException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Location doesnt exist.";
}