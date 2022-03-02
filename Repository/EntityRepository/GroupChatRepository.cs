using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.EntityRepository.Interfaces;

namespace Repository.EntityRepository
{
    public class GroupChatRepository : GenericRepository<GroupChatEntity>, IGroupChatRepository
    {
        public GroupChatRepository(DbContext dbContext) : base(dbContext) { }
    }
}