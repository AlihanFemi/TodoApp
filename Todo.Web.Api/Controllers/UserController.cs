﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Todo.Domain.Services;
using Todo.Web.Api.Models;

namespace Todo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult Get([FromRoute, Required] int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = _userService.GetUser(id.GetValueOrDefault());
            if (user == null)
            {
                return BadRequest("User with such id was not found");
            }

            return Ok(new { user.Id, user.Name });
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {

            var users = _userService.GetUsers();
            if (users == null)
            {
                return BadRequest("No users were found");
            }

            return Ok(
                users
                    .Select(user => new { user.Id, user.Name })
                    .ToArray()
                );
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody]CreateUserInput user)
        {
            if(string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Invalid input parameters");
            }
            _userService.Create(user.Name, user.Password);

            return Ok();
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser([FromRoute] int? id, [FromBody] UpdateUserInput user)
        {
            if (id is null || string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Invalid input parameters");
            }
            _userService.Update(id.GetValueOrDefault(), user.Name);

            return Ok();
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser([FromRoute]int? id)
        {
            if (id is null)
            {
                return BadRequest("Invalid id provided");
            }

            _userService.Delete(id.GetValueOrDefault());
            return Ok();
        }
    }
}
