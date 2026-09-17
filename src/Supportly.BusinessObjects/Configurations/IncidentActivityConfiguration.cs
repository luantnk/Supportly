using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supportly.BusinessObjects.Models;

namespace Supportly.BusinessObjects.Configurations;

public class IncidentActivityConfiguration : IEntityTypeConfiguration<IncidentActivity>
{
    public void Configure(EntityTypeBuilder<IncidentActivity> builder)
    {
        // Properties
        builder.Property(ia => ia.ActivityType)
            .HasMaxLength(100);

        builder.Property(ia => ia.Description)
            .HasMaxLength(500);
        
        builder.Property(ia => ia.OldValue)
            .HasMaxLength(200);
        
        builder.Property(ia => ia.NewValue)
            .HasMaxLength(200);
        
        // Relationships
        builder.HasOne(ia => ia.User)
            .WithMany(au => au.IncidentActivities)
            .HasForeignKey(ia => ia.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}