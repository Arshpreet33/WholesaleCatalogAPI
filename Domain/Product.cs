namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
        public required decimal UnitPrice { get; set; }
        public required double UnitWeight { get; set; }
        public required int ItemsInCase { get; set; }
        public required decimal CasePrice { get; set; }
        public int? ItemsInStock { get; set; }
        public int? CasesInStock { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
