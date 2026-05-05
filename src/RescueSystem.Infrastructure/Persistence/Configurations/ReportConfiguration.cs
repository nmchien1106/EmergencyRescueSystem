using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RescueSystem.Domain.Entities;

namespace RescueSystem.Infrastructure.Persistence.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.MissionId)
                .IsRequired();

            builder.Property(e => e.CreatedById)
                .IsRequired();

            builder.Property(e => e.Content)
                .HasMaxLength(5000);

            builder.Property(e => e.AttachmentUrl)
                .HasMaxLength(500);

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(e => e.Mission)
                .WithMany(m => m.Reports)
                .HasForeignKey(e => e.MissionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.CreatedBy)
                .WithMany(u => u.Reports)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
