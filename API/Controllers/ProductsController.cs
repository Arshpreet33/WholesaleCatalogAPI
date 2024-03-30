using Microsoft.AspNetCore.Mvc;
using Application.Products;

namespace API.Controllers
{
    public class ProductsController : BaseAPIController
    {

        [HttpGet]   //api/products
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{id}")]   //api/products/abcdefghijkl
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }));
        }
    }
}
