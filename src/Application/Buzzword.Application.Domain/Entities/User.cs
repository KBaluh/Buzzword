namespace Buzzword.Application.Domain.Entities
{
    public class User : GuidEntity
    {
        public string Name { get; set; } = "";

        public List<UserWord> UserWords { get; } = new();

        public List<Group> Groups { get; } = new();
    }
}
