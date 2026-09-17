using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supportly.BusinessObjects.Models;

namespace Supportly.BusinessObjects.Configurations;

public class IncidentJournalConfiguration : IEntityTypeConfiguration<IncidentJournal>
{
    public void Configure(EntityTypeBuilder<IncidentJournal> builder)
    {
        // Properties
        builder.Property(ij => ij.Content)
            .HasMaxLength(500);
    }
}