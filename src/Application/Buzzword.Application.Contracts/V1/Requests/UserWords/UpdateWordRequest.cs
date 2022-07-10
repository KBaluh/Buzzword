namespace Buzzword.Application.Contracts.V1.Requests.UserWords
{
    public class UpdateWordRequest
    {
        public string Word { get; set; } = "";

        public string Translate { get; set; } = "";
    }
}
