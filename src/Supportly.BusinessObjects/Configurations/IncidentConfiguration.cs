// Configurations/IncidentConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supportly.BusinessObjects.Models;

namespace Supportly.BusinessObjects.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        // Properties
        builder.Property(i => i.Number)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(i => i.Number)
            .IsUnique();

        builder.Property(i => i.ShortDescription)
            .HasMaxLength(500);

        builder.Property(i => i.ResolutionCode)
            .HasMaxLength(100);
        
        // Relationships
        builder.HasOne(i => i.ParentIncident)
            .WithMany() 
            .HasForeignKey(i => i.ParentIncidentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(i => i.AssignedTo)
            .WithMany() 
            .HasForeignKey(i => i.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        
        builder.HasOne(i => i.ResolvedBy)
            .WithMany()
            .HasForeignKey(i => i.ResolvedById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        
        builder.HasMany(i => i.IncidentActivities)
            .WithOne(ia => ia.Incident)
            .HasForeignKey(ia => ia.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(i => i.IncidentKnowledges)
            .WithOne(ik => ik.Incident)
            .HasForeignKey(ik => ik.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(i => i.IncidentWatchLists)
            .WithOne(iwl => iwl.Incident)
            .HasForeignKey(iwl => iwl.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.IncidentJournals)
            .WithOne(ij => ij.Incident)
            .HasForeignKey(ij => ij.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}