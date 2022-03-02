namespace Domain.Services.Interfaces
{
    public interface ISignalRService
    {
        public Task SendMessage(string user, string message);

        public Task CreateGroupChat(string connectionId, string codGroupChat);

        public Task SendMessageToGroup(string codGroupChat, string fromUser, string message);
    }
}