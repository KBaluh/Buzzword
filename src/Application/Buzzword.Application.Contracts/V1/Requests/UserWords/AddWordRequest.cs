namespace Buzzword.Application.Contracts.V1.Requests.UserWords
{
    public class AddWordRequest
    {
        public Guid UserId { get; set; }

        public string Word { get; set; } = "";

        public string Translate { get; set; } = "";
    }
}
