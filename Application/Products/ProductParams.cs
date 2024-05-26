using Application.Core;

namespace Application.Products
{
    public class ProductParams : Params
    {
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ManufacturerId { get; set; }
        public bool IsActive { get; set; }
    }
}
