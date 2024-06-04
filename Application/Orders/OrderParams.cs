using Application.Core;

namespace Application.Orders
{
    public class OrderParams : Params
    {
        public string OrderNumber { get; set; }
        public string UserName { get; set; }
        public bool IsApproved { get; set; }
        public Guid? ClientId { get; set; }
    }
}
