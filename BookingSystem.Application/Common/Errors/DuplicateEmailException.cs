using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class DuplicateEmailException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Email already exists";
}