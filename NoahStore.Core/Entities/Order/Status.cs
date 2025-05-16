using System.Runtime.Serialization;

namespace NoahStore.Core.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Processing")]
        Processing,
        [EnumMember(Value = "Shipped")]
        Shipped,
        [EnumMember(Value = "Delivered")]
        Delivered,
        [EnumMember(Value = "Completed")]
        Completed,
        [EnumMember(Value = "Returned")]
        Returned,
        [EnumMember(Value = "Cancelled")]
        Cancelled,
        [EnumMember(Value = "OnHold")]
        OnHold,


    }
    public enum PaymentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,
        [EnumMember(Value = "Refuned")]
        Refuned,
        [EnumMember(Value = "ApprovedForDelayedPayment")]
        ApprovedForDelayedPayment,
    }

}
