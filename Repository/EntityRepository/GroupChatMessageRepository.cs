using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.EntityRepository.Interfaces;

namespace Repository.EntityRepository
{
    public class GroupChatMessageRepository : GenericRepository<GroupChatMessageEntity>, IGroupChatMessageRepository
    {
        public GroupChatMessageRepository(DbContext dbContext) : base(dbContext) { }
    }
}