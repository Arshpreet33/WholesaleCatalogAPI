using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseAPIController
    {

    }
}
