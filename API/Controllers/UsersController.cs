using Application.Users;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseAPIController
    {
        [HttpGet]   //api/users
        public async Task<IActionResult> GetUsers([FromQuery] UserParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{username}")]   //api/users/username
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { UserName = username }));
        }

        [HttpPost]   //api/users
        public async Task<IActionResult> CreateUser(AppUser user)
        {
            return HandleResult(await Mediator.Send(new Create.Command { User = user }));
        }

        [HttpPut("{username}")]   //api/users/username
        public async Task<IActionResult> EditUser(string username, AppUser user)
        {
            user.UserName = username;
            return HandleResult(await Mediator.Send(new Edit.Command { User = user }));
        }

        [HttpPut("toggle/{username}")]   //api/users/toggle/username
        public async Task<IActionResult> ToggleActiveStatus(string username)
        {
            return HandleResult(await Mediator.Send(new ToggleActive.Command { UserName = username }));
        }
    }
}
