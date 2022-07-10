namespace Buzzword.Application.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/users/{userId}";
            public const string Create = Base + "/users";
            public const string Update = Base + "/users/{userId}";
            public const string Delete = Base + "/users/{userId}";
        }

        public static class UserWords
        {
            public const string GetAll = Base + "/userWords";
            public const string Get = Base + "/userWords/{userWordId}";
            public const string Create = Base + "/userWords";
            public const string Update = Base + "/userWords/{userWordId}";
            public const string Delete = Base + "/userWords/{userWordId}";
        }
    }
}
