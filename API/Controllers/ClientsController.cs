using Application.Clients;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API.Controllers
{
    public class ClientsController : BaseAPIController
    {
        [Authorize(Roles = "Admin,User")]
        [HttpGet]   //api/clients
        public async Task<IActionResult> GetClients([FromQuery] ClientParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]   //api/clients/abcdefghijkl
        public async Task<IActionResult> GetClientById(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]   //api/clients
        public async Task<IActionResult> CreateClient(Client client)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Client = client }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]   //api/clients/abcdefghijkl
        public async Task<IActionResult> EditClient(Guid id, Client client)
        {
            client.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Client = client }));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]   //api/clients/abcdefghijkl
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("toggle/{id}")]   //api/clients/toggle/abcdefghijkl
        public async Task<IActionResult> ToggleActiveStatus(Guid id)
        {
            return HandleResult(await Mediator.Send(new ToggleActive.Command { Id = id }));
        }
    }
}
