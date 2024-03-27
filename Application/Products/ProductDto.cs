namespace Application.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public double UnitPrice { get; set; }
        public double UnitWeight { get; set; }
        public int CaseQty { get; set; }
        public string Image { get; set; }
    }
}
