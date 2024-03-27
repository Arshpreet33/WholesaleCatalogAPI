using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Products;
using Microsoft.AspNetCore.Authorization;
using Application.Core;

namespace API.Controllers
{
    public class ProductsController : BaseAPIController
    {

        [HttpGet]   //api/products
        public async Task<IActionResult> GetActivities([FromQuery] ProductParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{id}")]   //api/products/abcdefghijkl
        public async Task<IActionResult> GetActivityById(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }));
        }
    }
}
