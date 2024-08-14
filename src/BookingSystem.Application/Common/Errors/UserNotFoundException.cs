using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class UserNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    // not displaying any information on purpose
    public string ErrorMessage => "";
}