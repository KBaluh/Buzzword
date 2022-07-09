using Buzzword.Common;
using Buzzword.HttpPolly;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Buzzword.DesktopApp.Services
{
    public class HttpPollyConnection : IHttpPollyConnection
    {
        private readonly IConfiguration _configuraiton;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpPollyConnection> _logger;

        public HttpPollyConnection(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<HttpPollyConnection> logger)
        {
            _configuraiton = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public Uri GetAppServiceString()
        {
            string url = _configuraiton.GetValue<string>("ApplicationUrl");
            return new Uri(url);
        }

        public Uri GetAuthServiceString()
        {
            string url = _configuraiton.GetValue<string>("AuthorizationUrl");
            return new Uri(url);
        }

        public HttpClient GetIdentityClient()
        {
            var client = _httpClientFactory.CreateClient("HttpPollyClient");
            return client;
        }

        public Task RefreshIfTokenExpiredAsync()
        {
            return Task.CompletedTask;
        }

        public Task RefreshTokenAsync()
        {
            return Task.CompletedTask;
        }

        public Task WriteAsync(LogType type, string source, string action, string message, string description)
        {
            if (type == LogType.Error)
            {
                _logger.LogError("Souce {source}, Action {action}, Message {message}, Description {description}",
                    source, action, message, description);
            }
            else if (type == LogType.Warning)
            {
                _logger.LogWarning("Souce {source}, Action {action}, Message {message}, Description {description}",
                    source, action, message, description);
            }
            else if (type == LogType.Information)
            {
                _logger.LogWarning("Souce {source}, Action {action}, Message {message}, Description {description}",
                    source, action, message, description);
            }

            return Task.CompletedTask;
        }

        public Task WriteAsync(LogType type, string source, string action, Exception ex)
        {
            _logger.LogCritical("LogType {type} Souce {source}, Action {action}, Exception {exception}",
                    type, source, action, ex);

            return Task.CompletedTask;
        }
    }
}
