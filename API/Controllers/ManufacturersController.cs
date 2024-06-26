﻿using Application.Manufacturers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ManufacturersController : BaseAPIController
    {
        [Authorize(Roles = "Admin,User")]
        [HttpGet]   //api/manufacturers
        public async Task<IActionResult> GetManufacturers([FromQuery] ManufacturerParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]   //api/manufacturers/abcdefghijkl
        public async Task<IActionResult> GetManufacturerById(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query() { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]   //api/manufacturers
        public async Task<IActionResult> CreateManufacturer(Manufacturer manufacturer)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Manufacturer = manufacturer }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]   //api/manufacturers/abcdefghijkl
        public async Task<IActionResult> EditManufacturer(Guid id, Manufacturer manufacturer)
        {
            manufacturer.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Manufacturer = manufacturer }));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]   //api/manufacturers/abcdefghijkl
        public async Task<IActionResult> DeleteManufacturer(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("toggle/{id}")]   //api/manufacturers/toggle/abcdefghijkl
        public async Task<IActionResult> ToggleActiveStatus(Guid id)
        {
            return HandleResult(await Mediator.Send(new ToggleActive.Command { Id = id }));
        }
    }
}
