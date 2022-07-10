using Buzzword.Application.Contracts.V1.Requests.UserWords;
using Buzzword.Common.Extensions;

namespace Buzzword.Application.Contracts.V1
{
    public static class UriRoutes
    {
        public static class Users
        {
            public static Uri GetAll(Uri baseUri) => new Uri(baseUri, ApiRoutes.Users.GetAll);
            public static Uri Get(Uri baseUri, Guid userId) => new Uri(baseUri, ApiRoutes.Users.Get.Replace("{userId}", userId.ToString()));
            public static Uri Create(Uri baseUri) => new Uri(baseUri, ApiRoutes.Users.Create);
            public static Uri Update(Uri baseUri, Guid userId) => new Uri(baseUri, ApiRoutes.Users.Get.Replace("{userId}", userId.ToString()));
            public static Uri Delete(Uri baseUri, Guid userId) => new Uri(baseUri, ApiRoutes.Users.Get.Replace("{userId}", userId.ToString()));
        }

        public static class UserWords
        {
            public static Uri GetAll(Uri baseUri, UserWordListQuery query)
            {
                var uri = new Uri(baseUri, ApiRoutes.UserWords.GetAll);
                if (query != null)
                {
                    uri = uri.AddParameter(nameof(query.UserId), query.UserId);
                }
                return uri;
            }

            public static Uri Get(Uri baseUri, Guid userWordId) => new Uri(baseUri, ApiRoutes.UserWords.Get.Replace("{userWordId}", userWordId.ToString()));
            public static Uri Create(Uri baseUri) => new Uri(baseUri, ApiRoutes.UserWords.Create);
            public static Uri Update(Uri baseUri, Guid userWordId) => new Uri(baseUri, ApiRoutes.UserWords.Update.Replace("{userWordId}", userWordId.ToString()));
            public static Uri Delete(Uri baseUri, Guid userWordId) => new Uri(baseUri, ApiRoutes.UserWords.Update.Replace("{userWordId}", userWordId.ToString()));
        }
    }
}
