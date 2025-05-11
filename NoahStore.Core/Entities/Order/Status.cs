namespace NoahStore.Core.Entities.Order
{
    public enum Status
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Completed,
        Returned,
        PaymentReceived,
        PaymentFailed,
        Cancelled,
        Refuned,
        OnHold,
        ApprovedForDelayedPayment,
        
    }
}
