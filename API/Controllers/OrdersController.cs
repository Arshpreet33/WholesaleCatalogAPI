using Application.Orders;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class OrdersController : BaseAPIController
    {
        [Authorize(Roles = "Admin, User")]
        [HttpGet]   //api/orders
        public async Task<IActionResult> GetOrders([FromQuery] OrderParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]  //api/orders/abcdefghijkl
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [Authorize(Roles = "User")]
        [HttpGet("myorders")]   //api/orders/myorders
        public async Task<IActionResult> GetMyOrders([FromQuery] OrderParams param)
        {
            // Get the logged-in user's username
            var userName = User.FindFirstValue(ClaimTypes.Name);
            param.UserName = userName;
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [Authorize(Roles = "User")]
        [HttpPost]   //api/orders
        public async Task<IActionResult> CreateOrder(Order order)
        {
            // Get the logged-in user's username
            var userName = User.FindFirstValue(ClaimTypes.Name);
            order.UserName = userName;
            return HandleResult(await Mediator.Send(new Create.Command { Order = order }));
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("items")]   //api/orders/orderitems
        public async Task<IActionResult> GetOrderItems([FromQuery] OrderItemParams param)
        {
            return HandlePagedResult(await Mediator.Send(new OrderItemsList.Query { Params = param }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("approve/{id}")]   //api/orders/approve/abcdefghijkl
        public async Task<IActionResult> ApproveOrder(Guid id)
        {
            return HandleResult(await Mediator.Send(new Approve.Command { Id = id }));
        }
    }
}
