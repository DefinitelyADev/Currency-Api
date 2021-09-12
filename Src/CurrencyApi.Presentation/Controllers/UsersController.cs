using System;
using System.Threading.Tasks;
using CurrencyApi.Application.Extensions;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.Currency;
using CurrencyApi.Application.Requests.User;
using CurrencyApi.Application.Responses;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.UserResults;
using CurrencyApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Presentation.Controllers
{
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => _userService = userService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserRequest request)
        {
            PagedResult<User> result = await _userService.GetAsync(request);

            if (result.Succeeded)
                return Ok(result.ToResponse());

            return BadRequest(new Response<GetUserRequest>("Could not get user.").WithErrors(result.Errors).WithData(request));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            User result = await _userService.GetByIdAsync(id.ToString());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            CreateUserResult result = await _userService.CreateAsync(request);

            if (result.Data != null && result.Succeeded)
                return CreatedAtAction(nameof(GetById), new {id = result.Data.Id}, result.Data);

            return BadRequest(new Response<CreateUserRequest>("Could not create user.").WithErrors(result.Errors).WithData(request));

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordRequest request)
        {
            string? username = HttpContext.User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                return BadRequest(new Response<CreateUserRequest>("Username is invalid."));

            UpdatePasswordResult result = await _userService.UpdatePasswordAsync(username, request);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(new Response<ChangePasswordRequest>("Could not update user password.").WithErrors(result.Errors).WithData(request));

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteUserResult result = await _userService.DeleteAsync(id.ToString());

            if (result.Succeeded)
                return NoContent();

            return BadRequest(new Response<object>($"Could not delete user.").WithErrors(result.Errors).WithData(new {Id = id}));
        }
    }
}
