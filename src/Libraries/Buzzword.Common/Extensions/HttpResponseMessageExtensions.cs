using System;
using System.Net;
using System.Net.Http;

namespace Buzzword.Common.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Проверка на наличие заголовка Token-Expired
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static bool IsTokenExpired(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (responseMessage.Headers.TryGetValues("Token-Expired", out _))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Проверка на <see cref="HttpStatusCode.Forbidden"/>
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static bool IsForbidden(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            return responseMessage.StatusCode == HttpStatusCode.Forbidden;
        }

        /// <summary>
        /// Проверка на <see cref="HttpStatusCode.Unauthorized"/>
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static bool IsUnauthorized(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            return responseMessage.StatusCode == HttpStatusCode.Unauthorized;
        }

        /// <summary>
        /// Проверка на <see cref="HttpStatusCode.InternalServerError"/>
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static bool IsServerError(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            return responseMessage.StatusCode == HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Проверка на <see cref="HttpStatusCode.NotFound"/>
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static bool IsNotFoundError(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            return responseMessage.StatusCode == HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Проверка на <see cref="HttpStatusCode.MethodNotAllowed"/>
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static bool IsMethodNotAllowed(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            return responseMessage.StatusCode == HttpStatusCode.MethodNotAllowed;
        }

        /// <summary>
        /// Проверка на <see cref="HttpStatusCode.RequestTimeout"/>
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static bool IsTimeout(this HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            return responseMessage.StatusCode == HttpStatusCode.RequestTimeout;
        }
    }
}
