using Repository.Entity.Interfaces;

namespace Repository.Entity
{
    public class GroupChatMessageEntity : IEntity
    {
        public GroupChatMessageEntity(int codGroupChat, string fromUser, string message)
        {
            CodGroupChat = codGroupChat;
            FromUser = fromUser;
            Message = message;
        }

        public int Id { get; }
        public int CodGroupChat { get; internal set; }
        public string FromUser { get; internal set; }
        public string Message { get; internal set; }
        public DateTime CreatedDate { get; internal set; }

        public virtual GroupChatEntity GroupChat { get; set; }
    }
}