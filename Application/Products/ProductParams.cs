using Application.Core;

namespace Application.Products
{
    public class ProductParams : PagingParams
    {
        public string TitleSearch { get; set; }
        public string Category { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
