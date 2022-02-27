using Repository.EntityRepository.Interfaces;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IChatRoomRepository ChatRoomRepository { get; }
    }
}