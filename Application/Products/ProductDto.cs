using Application.Categories;

namespace Application.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public double UnitWeight { get; set; }
        public int ItemsInCase { get; set; }
        public decimal CasePrice { get; set; }
        public int ItemsInStock { get; set; }
        public int CasesInStock { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public CategoryDto Category { get; set; }
    }
}
