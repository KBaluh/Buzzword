using Buzzword.Application.Domain.DataContext;
using Buzzword.Application.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Buzzword.Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IApplicationDataSource _dataSource;

        public UsersController(ILogger<UsersController> logger, IApplicationDataSource applicationDataSource)
        {
            _logger = logger;
            _dataSource = applicationDataSource;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
        {
            var items = await _dataSource.Users
                .Select(user => new User
                {
                    Id = user.Id,
                    Name = user.Name
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return Ok(items);
        }

        [HttpGet("{userId}", Name = nameof(GetUserAsync))]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var item = await _dataSource.Users
                .Where(user => user.Id == userId)
                .Select(user => new User
                {
                    Id = user.Id,
                    Name = user.Name
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            return Ok(item ?? new User());
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserAsync(User user)
        {
            _dataSource.Entry(user).State = EntityState.Added;
            await _dataSource.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserAsync), new { userId = user.Id });
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, User user)
        {
            var item = await _dataSource.Users.FirstAsync(user => user.Id == userId);
            item.Name = user.Name;

            await _dataSource.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            var item = await _dataSource.Users.FirstAsync(user => user.Id == userId);
            _dataSource.Users.Remove(item);
            await _dataSource.SaveChangesAsync();

            return Ok();
        }
    }
}
