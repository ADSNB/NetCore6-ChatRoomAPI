using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.TypeConfiguration
{
    public class GroupChatMessageTypeConfiguration : IEntityTypeConfiguration<GroupChatMessageEntity>
    {
        private readonly string _schema;

        public GroupChatMessageTypeConfiguration(string schema)
        { _schema = schema; }

        public void Configure(EntityTypeBuilder<GroupChatMessageEntity> builder)
        {
            builder.ToTable("GroupChatMessage", _schema);
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.GroupChat).WithMany(s => s.GroupChatMessage).HasForeignKey(s => s.CodGroupChat);

            builder.Property(e => e.Id)
                .HasColumnName("IdGroupChatMessage")
                .HasComment("Group chat message ID");

            builder.Property(e => e.CodGroupChat)
                .HasColumnType("int")
                .IsRequired()
                .HasComment("FK from GroupChat");

            builder.Property(e => e.FromUser)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
                .HasComment("User that sent the message");

            builder.Property(e => e.Message)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
                .HasComment("Message sent");

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetDate()")
                .HasComment("Record creation date");
        }
    }
}