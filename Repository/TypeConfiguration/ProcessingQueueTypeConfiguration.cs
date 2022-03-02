using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.TypeConfiguration
{
    public class ProcessingQueueTypeConfiguration : IEntityTypeConfiguration<ProcessingQueueEntity>
    {
        private readonly string _schema;

        public ProcessingQueueTypeConfiguration(string schema)
        { _schema = schema; }

        public void Configure(EntityTypeBuilder<ProcessingQueueEntity> builder)
        {
            builder.ToTable("ProcessingQueue", _schema);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("IdProcessingQueue")
                .HasComment("Processing queue ID");

            builder.Property(e => e.CodGroupChat)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasComment("GroupChat id, for message callback");

            builder.Property(e => e.CommandName)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasComment("Executed command name");

            builder.Property(e => e.CreatedByUser)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
                .HasComment("Created / requested by user");

            builder.Property(e => e.CodProcessingQueueStatus)
                .HasColumnType("int")
                .IsRequired()
                .HasComment("Process execution status");

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GetDate()")
                .HasComment("Record creation date");
        }
    }
}