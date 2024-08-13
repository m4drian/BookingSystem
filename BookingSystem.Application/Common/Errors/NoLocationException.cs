using System.Net;

namespace BookingSystem.Application.Authentication.Common.Errors;

public class NoLocationException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Location doesnt exist.";
}