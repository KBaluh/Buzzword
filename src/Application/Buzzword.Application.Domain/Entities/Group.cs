namespace Buzzword.Application.Domain.Entities
{
    public class Group : GuidEntity
    {
        public string Name { get; set; } = "";

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public List<GroupWord> GroupWords { get; } = new();
    }
}
