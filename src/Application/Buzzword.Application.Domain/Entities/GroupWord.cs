namespace Buzzword.Application.Domain.Entities
{
    public class GroupWord : GuidEntity
    {
        public Guid GroupId { get; set; }

        public Group? Group { get; set; }

        public Guid UserWordId { get; set; }

        public UserWord? UserWord { get; set; }
    }
}
