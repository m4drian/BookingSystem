using System.Net;

namespace BookingSystem.Application.Common.Errors;

public class CancellationTooEarlyException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Booking cannot be cancelled less than 24 hours before the reservation.";
}