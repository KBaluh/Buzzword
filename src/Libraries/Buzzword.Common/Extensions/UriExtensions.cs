using System;
using System.Globalization;
using System.Web;

namespace Buzzword.Common.Extensions
{
    public static class UriExtensions
    {
        public static Uri AddParameter(this Uri uri, string paramName, decimal? paramValue)
        {
            if (paramValue.HasValue)
            {
                return AddParameter(uri, paramName, paramValue.Value);
            }
            return uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, decimal paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue.ToString(CultureInfo.InvariantCulture);
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, float? paramValue)
        {
            if (paramValue.HasValue)
            {
                return AddParameter(uri, paramName, paramValue.Value);
            }
            return uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, float paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue.ToString(CultureInfo.InvariantCulture);
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, double? paramValue)
        {
            if (paramValue.HasValue)
            {
                return AddParameter(uri, paramName, paramValue.Value);
            }
            return uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, double paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue.ToString(CultureInfo.InvariantCulture);
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, int? paramValue)
        {
            if (paramValue.HasValue)
            {
                return AddParameter(uri, paramName, paramValue.Value);
            }
            return uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, int paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue.ToString();
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, long? paramValue)
        {
            if (paramValue.HasValue)
            {
                return AddParameter(uri, paramName, paramValue.Value);
            }
            return uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, long paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue.ToString();
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, Guid? paramValue)
        {
            if (paramValue.IsNotEmpty())
            {
                return AddParameter(uri, paramName, paramValue.Value);
            }
            return uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, Guid paramValue)
        {
            if (paramValue.IsEmpty())
            {
                return uri;
            }

            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue.ToString();
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, DateTimeOffset paramValue)
        {
            if (paramValue.IsEmpty())
            {
                return uri;
            }

            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz");
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, bool? paramValue)
        {
            return paramValue.HasValue ? AddParameter(uri, paramName, paramValue.Value) : uri;
        }

        public static Uri AddParameter(this Uri uri, string paramName, bool paramValue)
        {
            return paramValue ? AddParameter(uri, paramName, "true") : AddParameter(uri, paramName, "false");
        }

        public static Uri AddParameter(this Uri uri, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
