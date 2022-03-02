namespace Domain.Models.GroupChat
{
    public class GroupChatModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LastReceivedMessage { get; set; }
    }
}