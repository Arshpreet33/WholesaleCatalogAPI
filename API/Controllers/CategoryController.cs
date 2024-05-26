using Application.Categories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : BaseAPIController
    {
        [Authorize(Roles = "Admin,User")]
        [HttpGet]   //api/category
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]   //api/category/abcdefghijkl
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]   //api/category
        public async Task<IActionResult> CreateCategory(Category category)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Category = category }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]   //api/category/abcdefghijkl
        public async Task<IActionResult> EditCategory(Guid id, Category category)
        {
            category.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Category = category }));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]   //api/category/abcdefghijkl
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("toggle/{id}")]   //api/category/toggle/abcdefghijkl
        public async Task<IActionResult> ToggleActiveStatus(Guid id)
        {
            return HandleResult(await Mediator.Send(new ToggleActive.Command { Id = id }));
        }
    }
}
