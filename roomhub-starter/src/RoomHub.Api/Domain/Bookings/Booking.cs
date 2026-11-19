using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Domain.Bookings;

public sealed class Booking
{
    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateRange Range { get; private set; } = null!;
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    private Booking() { }

    public static Result<Booking> Create(Guid roomId, Room room, string bookedBy, DateTime startUtc, DateTime endUtc, int attendees, TimeProvider time)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return BookingErrors.EmptyBookedBy;

        if (attendees <= 0 || attendees > room.Capacity)
            return BookingErrors.InvalidAttendees;

        var rangeResult = DateRange.Create(startUtc, endUtc, time);
        if (rangeResult.IsFailure)
            return rangeResult.Error;

        return new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            BookedBy = bookedBy.Trim(),
            Range = rangeResult.Value,
            Attendees = attendees,
            Status = BookingStatus.Active
        };
    }

    public Result<Unit> Cancel()
    {
        if (Status == BookingStatus.Cancelled)
            return BookingErrors.AlreadyCancelled;

        Status = BookingStatus.Cancelled;
        return Unit.Value;
    }
}