using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

using Buzzword.Common;
using Buzzword.Common.Exceptions;
using Buzzword.Common.Extensions;

using Polly;

namespace Buzzword.HttpPolly
{
    public class HttpPollyClient : IHttpPollyClient
    {
        private readonly IHttpPollyConnection _connection;
        private readonly string _callerTypeName;
        private readonly Random _jitterer;

        public HttpPollyClient(IHttpPollyConnection connection, [CallerMemberName] string callerTypeName = "")
        {
            _connection = connection;
            _callerTypeName = callerTypeName;
            _jitterer = new Random();
        }

        public async Task<T?> GetResultAsync<T>(Uri uri, int retries = 3)
        {
            return await GetResultAsync<T>(uri, CancellationToken.None, retries);
        }

        public async Task<T?> GetResultAsync<T>(Uri uri, CancellationToken cancellationToken, int retries = 3)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsUnauthorized() || r.IsTimeout() || r.IsServerError())
                .WaitAndRetryAsync(retryCount: retries,
                    retryAttempt => TimeSpan.FromSeconds(retryAttempt) + TimeSpan.FromMilliseconds(_jitterer.Next(0, 500)),
                    onRetryAsync: async (outcome, timespan, retryNumber, context) =>
                    {
                        string description = $"RetryNumber: {retryNumber} with timespan {timespan} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                        await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(GetResultAsync), "Retry", description);
                        await _connection.RefreshTokenAsync();
                    });

            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.GetAsync(uri, cancellationToken);
            });

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(jsonStr);
            }
            else if (response.IsNotFoundError())
            {
                return default;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `GET` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(GetResultAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(GetResultAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? response.ToString());
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(GetResultAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(GetResultAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task<TResult?> PostResultAsync<TResult>(Uri uri, int retries = 1)
        {
            return await PostResultAsync<TResult>(uri, CancellationToken.None, retries);
        }

        public async Task<TResult?> PostResultAsync<TResult>(Uri uri, CancellationToken cancellationToken, int retries = 1)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsTimeout() || r.IsUnauthorized())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(PostResultAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.PostAsync(uri, content, cancellationToken);
            });

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(jsonStr);
            }
            else if (response.IsNotFoundError())
            {
                return default;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `POST` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task<TResult?> PostResultAsync<TResult>(Uri uri, TResult param, int retries = 1)
        {
            return await PostResultAsync<TResult>(uri, param, CancellationToken.None, retries);
        }

        public async Task<TResult?> PostResultAsync<TResult>(Uri uri, TResult param, CancellationToken cancellationToken, int retries = 1)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsTimeout() || r.IsUnauthorized())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(PostResultAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");
            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.PostAsync(uri, content, cancellationToken);
            });

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(jsonStr);
            }
            else if (response.IsNotFoundError())
            {
                return default;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `POST` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task<TResult?> PostResultAsync<TResult, TParam>(Uri uri, TParam param, int retries = 1)
        {
            return await PostResultAsync<TResult, TParam>(uri, param, CancellationToken.None, retries);
        }

        public async Task<TResult?> PostResultAsync<TResult, TParam>(Uri uri, TParam param, CancellationToken cancellationToken, int retries = 1)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsTimeout() || r.IsUnauthorized())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(PostResultAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");
            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.PostAsync(uri, content, cancellationToken);
            });

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(jsonStr);
            }
            else if (response.IsNotFoundError())
            {
                return default;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `POST` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostResultAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task PostAsync(Uri uri, int retries = 1)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsTimeout() || r.IsUnauthorized())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(PostAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var content = new StringContent(JsonSerializer.Serialize(string.Empty), Encoding.UTF8, "application/json");
            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.PostAsync(uri, content);
            });

            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else if (response.IsNotFoundError())
            {
                return;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `POST` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task PostAsync<T>(Uri uri, T param, int retries = 1)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsTimeout() || r.IsUnauthorized())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(PostAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");
            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.PostAsync(uri, content);
            });

            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else if (response.IsNotFoundError())
            {
                return;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `POST` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? response.ToString());
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(PostAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task<TParam?> UpdateResultAsync<TParam>(Uri uri, TParam param, int retries = 3)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsUnauthorized() || r.IsTimeout() || r.IsServerError())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(UpdateResultAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");

            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.PutAsync(uri, content);
            });

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TParam>(jsonStr);
            }
            else if (response.IsNotFoundError())
            {
                return default;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `PUT` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task<TResult?> UpdateResultAsync<TResult, TParam>(Uri uri, TParam param, int retries = 3)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsUnauthorized() || r.IsTimeout() || r.IsServerError())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(UpdateResultAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");

            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.PutAsync(uri, content);
            });

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(jsonStr);
            }
            else if (response.IsNotFoundError())
            {
                return default;
            }

            if (response.IsTokenExpired())
            {
                await _connection.RefreshTokenAsync();
                return await UpdateResultAsync<TResult, TParam>(uri, param);
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `PUT` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(UpdateResultAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task<bool> DeleteResultAsync(Uri uri, int retries = 3)
        {
            await _connection.RefreshIfTokenExpiredAsync();

            var authorizationEnsuringPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.IsTokenExpired() || r.IsUnauthorized() || r.IsTimeout() || r.IsServerError())
                .RetryAsync(retryCount: retries, onRetryAsync: async (outcome, retryNumber, context) =>
                {
                    string description = $"RetryNumber: {retryNumber} for {uri}{Environment.NewLine}Exception: {outcome?.Exception}{Environment.NewLine}Result: {outcome?.Result}";
                    await _connection.WriteAsync(LogType.Information, _callerTypeName, nameof(DeleteResultAsync), "Retry", description);
                    await _connection.RefreshTokenAsync();
                });

            var response = await authorizationEnsuringPolicy.ExecuteAsync(async () =>
            {
                var httpClient = _connection.GetIdentityClient();
                return await httpClient.DeleteAsync(uri);
            });

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.IsNotFoundError())
            {
                return false;
            }

            if (response.IsMethodNotAllowed())
            {
                string wrongMethodType = "Не правильный тип `DELETE` для вызываемоего маршрута " + uri;
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(DeleteResultAsync), "MethodNotAllowed", wrongMethodType);
                throw new BuzzwordException(wrongMethodType);
            }

            var httpContent = await response.Content.ReadAsStringAsync();

            string noAccessMessage = "Нет доступа к действию " + uri;
            if (response.IsForbidden())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(DeleteResultAsync), "Forbidden", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            if (response.IsUnauthorized())
            {
                await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(DeleteResultAsync), "Unauthorized", noAccessMessage);
                throw new BuzzwordAccessException(httpContent ?? noAccessMessage);
            }

            await _connection.WriteAsync(LogType.Error, _callerTypeName, nameof(DeleteResultAsync), response.StatusCode.ToString(),
                $"Для действия {uri} возникла ошибка {Environment.NewLine}{httpContent ?? response.ToString()}");
            throw new BuzzwordException(httpContent ?? response.ToString());
        }

        public async Task<int> DeleteRemotesAsync(params Uri[] deleteUries)
        {
            int affectedRows = 0;

            foreach (var deleteUri in deleteUries)
            {
                bool result = await DeleteResultAsync(deleteUri);
                if (result)
                {
                    affectedRows += 1;
                }
            }

            return affectedRows;
        }
    }
}
