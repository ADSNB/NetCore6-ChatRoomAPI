using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.EntityRepository.Interfaces;

namespace Repository.EntityRepository
{
    public class ChatRoomRepository : GenericRepository<ChatRoomEntity>, IChatRoomRepository
    {
        public ChatRoomRepository(DbContext dbContext) : base(dbContext) { }
    }
}