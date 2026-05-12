using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Infrastructure.Persistence.Configurations
{
    public class ChecklistConfiguration : IEntityTypeConfiguration<Checklist>
    {
        public void Configure(EntityTypeBuilder<Checklist> builder)
        {
            builder.ToTable("Checklists");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.MissionId)
                .IsRequired();

            builder.Property(e => e.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(e => e.Mission)
                .WithMany(m => m.Checklists)
                .HasForeignKey(e => e.MissionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.ChecklistItems)
                .WithOne(i => i.Checklist)
                .HasForeignKey(i => i.ChecklistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
