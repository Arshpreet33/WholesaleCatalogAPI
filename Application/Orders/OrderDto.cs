using Application.Clients;
using Application.Users;
using Domain;

namespace Application.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public ClientDto Client { get; set; }
        public string UserName { get; set; }
        public UserDto User { get; set; }
        public decimal SubTotal { get; set; }
        public int ItemsCount { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; }
    }
}
