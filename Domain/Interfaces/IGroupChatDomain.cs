using Domain.InputModel.GroupChat;
using Domain.Models.GroupChat;

namespace Domain.Interfaces
{
    public interface IGroupChatDomain
    {
        public List<GroupChatModel> GetAllGroupChat();
        public bool CreateGroupChat(CreateGroupChatInputModel inputModel);
        public void AddUserToGroupChat(string connectionId, string groupName);
    }
}
