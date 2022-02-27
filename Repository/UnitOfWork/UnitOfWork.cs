using Repository.EntityRepository.Interfaces;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly NetCoreChatRoomAPIDbContext _NetCoreChatRoomAPIDbContext;

        private bool disposed = false;

        public UnitOfWork(NetCoreChatRoomAPIDbContext netCoreChatRoomAPIDbContext)
        {
            _NetCoreChatRoomAPIDbContext = netCoreChatRoomAPIDbContext;
            this.InitializeRepository(netCoreChatRoomAPIDbContext);
        }

        public IChatRoomRepository ChatRoomRepository { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _NetCoreChatRoomAPIDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void Save() => _NetCoreChatRoomAPIDbContext.SaveChanges();
    }
}