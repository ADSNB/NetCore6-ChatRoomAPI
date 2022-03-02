namespace Domain.Models.GroupChatMessage
{
    public class GroupChatMessageModel
    {
        public int Id { get; set; }
        public int CodGroupChat { get; set; }
        public string FromUser { get; set; }
        public string Message { get; set; }
    }
}