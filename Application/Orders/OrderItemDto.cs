using Application.Products;

namespace Application.Orders
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public OrderDto Order { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool ByCase { get; set; }
    }
}
