namespace Buzzword.Application.Domain.Entities
{
    public class UserWord : BaseEntity<Guid>
    {
        public string Word { get; set; } = "";

        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
