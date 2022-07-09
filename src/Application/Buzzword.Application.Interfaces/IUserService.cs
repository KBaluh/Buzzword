using Buzzword.Application.Contracts.V1.Responses;

namespace Buzzword.Application.Interfaces
{
    public interface IUserService
    {
        Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken);
        Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken);
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserDto> UpdateUserAsync(UserDto user);
        Task DeleteUserASync(Guid userId);
    }
}
