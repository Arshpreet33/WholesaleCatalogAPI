using Application.Core;

namespace Application.Orders
{
    public class OrderItemParams : Params
    {
        public Guid OrderId { get; set; }
    }
}
