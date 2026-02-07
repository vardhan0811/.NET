namespace QuickBite.Domain.Enums
{
    public enum OrderStatus
    {
        Created,
        PaymentPending,
        PaymentProcessing,
        PaymentFailed,
        PaymentSuccessful,
        RestaurantAccepted,
        RestaurantPreparing,
        RestaurantReady,
        DriverAssigned,
        DriverPickedUp,
        OutForDelivery,
        Delivered,
        Cancelled,
        Refunded
    }
}
