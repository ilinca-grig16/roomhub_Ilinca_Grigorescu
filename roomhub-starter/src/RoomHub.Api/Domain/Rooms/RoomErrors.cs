using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Rooms;

public static class RoomErrors
{
    public static readonly Error EmptyName = Error.Validation("Room.EmptyName", "A room name is needed.");
    public static readonly Error InvalidCapacity = Error.Validation("Room.InvalidCapacity", "Capacity must be positive.");
    public static readonly Error NotFound = Error.NotFound("Room.NotFound", "Room was not found.");
}