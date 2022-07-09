using Buzzword.Application.Contracts.V1.Responses;

namespace Buzzword.Application.Interfaces
{
    public interface IUserService
    {
        Task<IList<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserAsync(Guid userId);
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserDto> UpdateUserAsync(UserDto user);
        Task<UserDto> DeleteUserASync(Guid userId);
    }
}
