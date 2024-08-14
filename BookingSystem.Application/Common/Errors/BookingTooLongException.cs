using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class BookingTooLongException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Booking cannot exceed one week.";
}