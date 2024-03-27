using Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : BaseAPIController
    {
        [HttpGet]   //api/products
        public async Task<IActionResult> GetClients([FromQuery] ProductParams param)
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
