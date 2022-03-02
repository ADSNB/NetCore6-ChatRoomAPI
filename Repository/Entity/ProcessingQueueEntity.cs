using Repository.Entity.Interfaces;

namespace Repository.Entity
{
    public class ProcessingQueueEntity : IEntity
    {
        public ProcessingQueueEntity(int codGroupChat, string commandName, string createdByUser)
        {
            CodGroupChat = codGroupChat;
            CommandName = commandName;
            CreatedByUser = createdByUser;
            CodProcessingQueueStatus = 1;
        }

        public int Id { get; }
        public int CodGroupChat { get; internal set; }
        public string CommandName { get; internal set; }
        public string CreatedByUser { get; internal set; }
        public int CodProcessingQueueStatus { get; internal set; }
        public DateTime CreatedDate { get; internal set; }

        public void UpdateStatusInProgress() => CodProcessingQueueStatus = 2;

        public void UpdateStatusDone() => CodProcessingQueueStatus = 3;

        public void UpdateStatusFailed() => CodProcessingQueueStatus = 4;
    }
}