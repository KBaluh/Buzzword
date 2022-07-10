using Buzzword.Application.Contracts.V1;
using Buzzword.Application.Contracts.V1.Responses;
using Buzzword.Application.Interfaces;
using Buzzword.HttpPolly;

namespace Buzzword.Application.WebDomainServices
{
    public class UserService : IUserService
    {
        private readonly IHttpPollyConnection _connection;
        private readonly IHttpPollyClient _httpClient;

        public UserService(IHttpPollyConnection httpPollyConnection, IHttpPollyClient httpPollyClient)
        {
            _connection = httpPollyConnection;
            _httpClient = httpPollyClient;
        }

        public async Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            var applicaitonUri = _connection.GetAppServiceString();
            var uri = UriRoutes.Users.GetAll(applicaitonUri);
            return await _httpClient.GetResultAsync<IList<UserDto>>(uri, cancellationToken) ?? Array.Empty<UserDto>();
        }

        public async Task<UserDto> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var applicaitonUri = _connection.GetAppServiceString();
            var uri = UriRoutes.Users.Get(applicaitonUri, userId);
            return await _httpClient.GetResultAsync<UserDto>(uri, cancellationToken) ?? new UserDto();
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            var applicaitonUri = _connection.GetAppServiceString();
            var uri = UriRoutes.Users.Create(applicaitonUri);
            return await _httpClient.PostResultAsync<UserDto>(uri) ?? new UserDto();
        }

        public async Task<UserDto> UpdateUserAsync(UserDto user)
        {
            var applicaitonUri = _connection.GetAppServiceString();
            var uri = UriRoutes.Users.Update(applicaitonUri, user.Id);
            return await _httpClient.UpdateResultAsync(uri, user) ?? new UserDto();
        }

        public async Task DeleteUserASync(Guid userId)
        {
            var applicaitonUri = _connection.GetAppServiceString();
            var uri = UriRoutes.Users.Delete(applicaitonUri, userId);
            await _httpClient.DeleteResultAsync(uri);
        }
    }
}
