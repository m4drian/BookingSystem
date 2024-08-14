using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class RegisterValidationException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "All fields must not be empty. Password must be 16-32 characters long. Role must be either admin or employee.";
}