namespace Buzzword.Application.Domain.Entities
{
    public class Group
    {
        public string Name { get; set; } = "";

        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
