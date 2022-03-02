using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.EntityRepository.Interfaces;

namespace Repository.EntityRepository
{
    public class ProcessingQueueRepository : GenericRepository<ProcessingQueueEntity>, IProcessingQueueRepository
    {
        public ProcessingQueueRepository(DbContext dbContext) : base(dbContext) { }
    }
}