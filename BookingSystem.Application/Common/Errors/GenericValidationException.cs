using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class GenericValidationException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public string ErrorMessage => "Validation error.";
}