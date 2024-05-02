using System.ComponentModel;

namespace Order.Domain.Enums
{
    public enum OrderStatusEnum
    {
        [Description("In preparation")]
        InPreparation = 1,
            
        [Description("Ready")]
        Ready,

        [Description("Completed")]
        Completed,

        [Description("Canceled")]
        Canceled
    }
}
