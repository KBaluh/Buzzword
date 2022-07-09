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
    }
}
