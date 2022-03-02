using Repository.EntityRepository.Interfaces;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGroupChatRepository GroupChatRepository { get; }
        IGroupChatMessageRepository GroupChatMessageRepository { get; }
        IProcessingQueueRepository ProcessingQueueRepository { get; }
    }
}