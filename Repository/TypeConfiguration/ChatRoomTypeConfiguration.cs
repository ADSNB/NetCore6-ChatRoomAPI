using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.TypeConfiguration
{
    public class ChatRoomTypeConfiguration : IEntityTypeConfiguration<ChatRoomEntity>
    {
        private readonly string _schema;

        public ChatRoomTypeConfiguration(string schema)
        { _schema = schema; }

        public void Configure(EntityTypeBuilder<ChatRoomEntity> builder)
        {
            builder.ToTable("ChatRoom", _schema);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("IdChatRoom")
                .HasComment("Chat room ID");

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasComment("Chat room name");

            builder.Property(e => e.CreatedDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GetDate()")
                .HasComment("Record creation date");
        }
    }
}