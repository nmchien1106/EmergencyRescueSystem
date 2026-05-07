using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RescueSystem.Domain.Entities;

namespace RescueSystem.Infrastructure.Persistence.Configurations
{
    public class MissionHistoryConfiguration : IEntityTypeConfiguration<MissionHistory>
    {
        public void Configure(EntityTypeBuilder<MissionHistory> builder)
        {
            builder.ToTable("MissionHistories");

            builder.Property(mh => mh.Note)
                .HasMaxLength(1000);

            // Bắt buộc Mission
            builder.HasOne(mh => mh.Mission)
                .WithMany(m => m.MissionHistories)
                .HasForeignKey(mh => mh.MissionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bắt buộc Người thay đổi
            builder.HasOne(mh => mh.ChangedBy)
                .WithMany()
                .HasForeignKey(mh => mh.ChangedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
