using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Bookings;

public sealed record DateRange
{
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }

    private DateRange(DateTime startUtc, DateTime endUtc)
    {
        StartUtc = startUtc;
        EndUtc = endUtc;
    }

    public static Result<DateRange> Create(DateTime start, DateTime end, TimeProvider time)
    {
        var startUtc = start.Kind == DateTimeKind.Utc ? start : DateTime.SpecifyKind(start, DateTimeKind.Utc);
        var endUtc = end.Kind == DateTimeKind.Utc ? end : DateTime.SpecifyKind(end, DateTimeKind.Utc);

        if (endUtc <= startUtc)
            return BookingErrors.EndBeforeStart;

        var now = time.GetUtcNow().UtcDateTime;
        if (startUtc < now)
            return BookingErrors.StartInPast;

        if ((endUtc - startUtc).TotalHours > 4)
            return BookingErrors.ExceedsMaxDuration;

        return new DateRange(startUtc, endUtc);
    }

    public bool Overlaps(DateRange other) => StartUtc < other.EndUtc && other.StartUtc < EndUtc;
}