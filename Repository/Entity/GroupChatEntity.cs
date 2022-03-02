using Repository.Entity.Interfaces;

namespace Repository.Entity
{
    public class GroupChatEntity : IEntity
    {
        public GroupChatEntity(string name, string description, string createdByUser)
        {
            Name = name;
            Description = description;
            CreatedByUser = createdByUser;
        }

        public int Id { get; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string CreatedByUser { get; internal set; }
        public DateTime CreatedDate { get; internal set; }

        public virtual ICollection<GroupChatMessageEntity> GroupChatMessage { get; set; }
    }
}