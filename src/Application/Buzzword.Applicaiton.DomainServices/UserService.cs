using Buzzword.Application.Contracts.V1.Responses;
using Buzzword.Application.Domain.DataContext;
using Buzzword.Application.Domain.Entities;
using Buzzword.Application.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Buzzword.Applicaiton.DomainServices
{
    public class UserService : IUserService
    {
        private readonly IApplicationDataSource _dataSource;

        public UserService(IApplicationDataSource applicationDataSource)
        {
            _dataSource = applicationDataSource;
        }

        public async Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var items = await _dataSource.Users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return items;
        }

        public async Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var item = await _dataSource.Users
                .Where(user => user.Id == userId)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name
                })
                .AsNoTracking()
                .FirstAsync(cancellationToken);
            return item;
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            User entity = new User
            {
                Name = user.Name
            };

            _dataSource.Entry(entity).State = EntityState.Added;
            await _dataSource.SaveChangesAsync();

            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<UserDto> UpdateUserAsync(UserDto user)
        {
            User entity = await _dataSource.Users.FirstAsync(x => x.Id == user.Id);
            entity.Name = user.Name;

            await _dataSource.SaveChangesAsync();

            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task DeleteUserASync(Guid userId)
        {
            var entity = await _dataSource.Users.FirstAsync(x => x.Id == userId);
            _dataSource.Users.Remove(entity);
            await _dataSource.SaveChangesAsync();
        }
    }
}
