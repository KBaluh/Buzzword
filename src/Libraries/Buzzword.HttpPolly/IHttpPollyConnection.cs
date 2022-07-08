using System;
using System.Net.Http;
using System.Threading.Tasks;

using Buzzword.Common;

namespace Buzzword.HttpPolly
{
    public interface IHttpPollyConnection
    {
        Uri GetAuthServiceString();
        Uri GetAppServiceString();

        HttpClient GetIdentityClient();
        Task RefreshIfTokenExpiredAsync();
        Task RefreshTokenAsync();

        Task WriteAsync(LogType type, string source, string action, string message, string description);
        Task WriteAsync(LogType type, string source, string action, Exception ex);
    }
}
