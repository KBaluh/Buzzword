using Buzzword.Application.Contracts.V1;
using Buzzword.Application.Contracts.V1.Requests.UserWords;
using Buzzword.Application.Contracts.V1.Responses;
using Buzzword.Application.Interfaces;
using Buzzword.HttpPolly;

namespace Buzzword.Application.WebDomainServices
{
    public class UserWordService : IUserWordService
    {
        private readonly IHttpPollyConnection _connection;
        private readonly IHttpPollyClient _httpClient;

        public UserWordService(IHttpPollyConnection httpPollyConnection, IHttpPollyClient httpPollyClient)
        {
            _connection = httpPollyConnection;
            _httpClient = httpPollyClient;
        }

        public async Task<IList<UserWordDto>> GetUserWordsAsync(UserWordListQuery query, CancellationToken cancellationToken = default)
        {
            var applicationUri = _connection.GetAppServiceString();
            var uri = UriRoutes.UserWords.GetAll(applicationUri, query);
            return await _httpClient.GetResultAsync<IList<UserWordDto>>(uri, cancellationToken) ?? Array.Empty<UserWordDto>();
        }

        public async Task<UserWordDto> GetUserWordAsync(Guid userWordId, CancellationToken cancellationToken = default)
        {
            var applicationUri = _connection.GetAppServiceString();
            var uri = UriRoutes.UserWords.Get(applicationUri, userWordId);
            return await _httpClient.GetResultAsync<UserWordDto>(uri, cancellationToken) ?? new UserWordDto();
        }

        public async Task<Guid> AddWordAsync(AddWordRequest request)
        {
            var applicationUri = _connection.GetAppServiceString();
            var uri = UriRoutes.UserWords.Create(applicationUri);
            return await _httpClient.PostResultAsync<Guid, AddWordRequest>(uri, request);
        }

        public async Task<UserWordDto> UpdateWordAsync(Guid userWordId, UpdateWordRequest request)
        {
            var applicationUri = _connection.GetAppServiceString();
            var uri = UriRoutes.UserWords.Update(applicationUri, userWordId);
            return await _httpClient.UpdateResultAsync<UserWordDto, UpdateWordRequest>(uri, request) ?? new UserWordDto();
        }

        public async Task<bool> RemoveWordAsync(Guid userWordId)
        {
            var applicationUri = _connection.GetAppServiceString();
            var uri = UriRoutes.UserWords.Delete(applicationUri, userWordId);
            return await _httpClient.DeleteResultAsync(uri);
        }
    }
}
