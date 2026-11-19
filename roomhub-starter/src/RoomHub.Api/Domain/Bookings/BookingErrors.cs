using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Bookings;

public enum BookingStatus { Active, Cancelled }

public static class BookingErrors
{
    public static readonly Error EmptyBookedBy = Error.Validation("Booking.EmptyBookedBy", "bookedBy is required.");
    public static readonly Error EndBeforeStart = Error.Validation("Booking.EndBeforeStart", "endUtc must be strictly after startUtc.");
    public static readonly Error StartInPast = Error.Validation("Booking.StartInPast", "Booking cannot start in the past.");
    public static readonly Error ExceedsMaxDuration = Error.Validation("Booking.ExceedsMaxDuration", "Maximum duration is 4 hours.");
    public static readonly Error InvalidAttendees = Error.Validation("Booking.InvalidAttendees", "Attendees must be strictly positive and fit room capacity.");
    public static readonly Error Overlap = Error.Conflict("Booking.Overlap", "Room is already booked for this interval.");
    public static readonly Error AlreadyCancelled = Error.Conflict("Booking.AlreadyCancelled", "Booking is already cancelled.");
    public static readonly Error NotFound = Error.NotFound("Booking.NotFound", "Booking was not found.");
}