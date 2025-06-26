namespace Dentizone.Domain.Enums
{
    public enum ShipmentActivityStatus
    {
        Pending = 0,
        PickedUp = 1,  // New
        Shipped = 2,   // Corresponds to "In its way"
        Delivered = 3, // Corresponds to "Arrived"
        Cancelled = 4
    }
}