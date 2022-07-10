namespace Buzzword.Application.Contracts.V1.Responses
{
    public class UserWordDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Word { get; set; } = "";

        public string Translate { get; set; } = "";
    }
}
