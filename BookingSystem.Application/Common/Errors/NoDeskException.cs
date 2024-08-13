using System.Net;

namespace BookingSystem.Application.Authentication.Common.Errors;

public class NoDeskException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Desk doesnt exist.";
}