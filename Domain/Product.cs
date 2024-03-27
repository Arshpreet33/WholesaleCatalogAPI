namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Code { get; set; }
        public required double UnitPrice { get; set; }
        public required double UnitWeight { get; set; }
        public required int CaseQty { get; set; }
        public string? Image { get; set; }
    }
}
