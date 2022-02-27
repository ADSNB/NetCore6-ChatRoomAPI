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

        public DbSet<ChatRoomEntity> ChatRoom { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ChatRoomTypeConfiguration(_schema));
        }
    }
}