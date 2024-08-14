using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class CannotCancelEmptyDeskException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Cannot cancel empty desk.";
}