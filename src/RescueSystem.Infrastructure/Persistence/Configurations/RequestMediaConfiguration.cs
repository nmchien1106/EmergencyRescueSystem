using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Infrastructure.Persistence.Configurations
{
    public class RequestMediaConfiguration : IEntityTypeConfiguration<RequestMedia>
    {
        public void Configure(EntityTypeBuilder<RequestMedia> builder)
        {
            builder.ToTable("RequestMedia");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.RequestId)
                .IsRequired();

            builder.Property(e => e.SecureUrl)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.PublicId)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.ResourceType)
                .IsRequired()
                .HasDefaultValue(MediaType.Image);

            builder.HasOne(e => e.Request)
                .WithMany(r => r.Medias)
                .HasForeignKey(e => e.RequestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
