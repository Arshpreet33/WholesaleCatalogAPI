using Application.Core;

namespace Application.Categories
{
    public class CategoryParams : Params
    {
        public string Name { get; set; }
        public Guid? ManufacturerId { get; set; }
        public bool IsActive { get; set; }
    }
}
