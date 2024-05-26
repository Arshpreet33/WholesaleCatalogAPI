using Application.Products;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        [Authorize(Roles = "Admin,User")]
        [HttpGet]   //api/products
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]   //api/products/abcdefghijkl
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]   //api/products
        public async Task<IActionResult> CreateProduct(Product product)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Product = product }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]   //api/products/abcdefghijkl
        public async Task<IActionResult> EditProduct(Guid id, Product product)
        {
            product.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Product = product }));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]   //api/products/abcdefghijkl
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("toggle/{id}")]   //api/products/toggle/abcdefghijkl
        public async Task<IActionResult> ToggleActiveStatus(Guid id)
        {
            return HandleResult(await Mediator.Send(new ToggleActive.Command { Id = id }));
        }
    }
}