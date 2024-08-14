using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class InvalidPasswordException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Password is incorrect.";
}