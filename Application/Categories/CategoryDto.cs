using Application.Manufacturers;

namespace Application.Categories
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
    }
}
