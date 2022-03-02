using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.TypeConfiguration;

namespace Repository
{
    public class NetCoreChatRoomAPIDbContext : IdentityDbContext
    {
        private readonly string _schema = "dbo";

        public NetCoreChatRoomAPIDbContext(DbContextOptions<NetCoreChatRoomAPIDbContext> options)
        : base(options)
        {
        }

        public DbSet<GroupChatEntity> GroupChat { get; set; }
        public DbSet<GroupChatMessageEntity> GroupChatMessage { get; set; }
        public DbSet<ProcessingQueueEntity> ProcessingQueue { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new GroupChatTypeConfiguration(_schema));
            builder.ApplyConfiguration(new GroupChatMessageTypeConfiguration(_schema));
            builder.ApplyConfiguration(new ProcessingQueueTypeConfiguration(_schema));
        }
    }
}