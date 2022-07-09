using Buzzword.Application.Contracts.V1;
using Buzzword.Application.Contracts.V1.Responses;
using Buzzword.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Buzzword.Application.API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Users.GetAll)]
        [ProducesResponseType(typeof(IList<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
        {
            var items = await _userService.GetUsersAsync(cancellationToken);
            return Ok(items);
        }

        [HttpGet(ApiRoutes.Users.Get, Name = nameof(GetUserAsync))]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var item = await _userService.GetUserAsync(userId, cancellationToken);
            return Ok(item ?? new UserDto());
        }

        [HttpPost(ApiRoutes.Users.Create)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserAsync(UserDto user)
        {
            var item = await _userService.CreateUserAsync(user);
            return CreatedAtRoute(nameof(GetUserAsync), new { userId = user.Id }, user);
        }

        [HttpPut(ApiRoutes.Users.Update)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, UserDto user)
        {
            user.Id = userId;

            var item = await _userService.UpdateUserAsync(user);
            return Ok(item);
        }

        [HttpDelete(ApiRoutes.Users.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            await _userService.DeleteUserASync(userId);
            return Ok();
        }
    }
}
