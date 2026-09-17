using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supportly.BusinessObjects.Models;

namespace Supportly.BusinessObjects.Configurations;

public class IncidentWatchListConfiguration : IEntityTypeConfiguration<IncidentWatchList>
{
    public void Configure(EntityTypeBuilder<IncidentWatchList> builder)
    {
        // Relationships
        builder.HasOne(iwl => iwl.User)
            .WithMany(au => au.IncidentWatchLists)
            .HasForeignKey(iwl => iwl.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        // Prevent user watch a single incident twice
        builder.HasIndex(iwl => new { iwl.IncidentId, iwl.UserId })
            .IsUnique();
    }
}