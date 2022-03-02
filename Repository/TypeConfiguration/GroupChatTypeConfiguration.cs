using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.TypeConfiguration
{
    public class GroupChatTypeConfiguration : IEntityTypeConfiguration<GroupChatEntity>
    {
        private readonly string _schema;

        public GroupChatTypeConfiguration(string schema)
        { _schema = schema; }

        public void Configure(EntityTypeBuilder<GroupChatEntity> builder)
        {
            builder.ToTable("GroupChat", _schema);
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.GroupChatMessage).WithOne(e => e.GroupChat);

            builder.Property(e => e.Id)
                .HasColumnName("IdGroupChat")
                .HasComment("Group chat ID");

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasComment("Group chat name");

            builder.Property(e => e.Description)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
                .HasComment("Group chat description");

            builder.Property(e => e.CreatedByUser)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
                .HasComment("Group chat description");

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetDate()")
                .HasComment("Record creation date");
        }
    }
}