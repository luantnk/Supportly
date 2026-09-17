using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supportly.BusinessObjects.Models;

namespace Supportly.BusinessObjects.Configurations;

public class IncidentKnowledgeConfiguration : IEntityTypeConfiguration<IncidentKnowledge>
{
    public void Configure(EntityTypeBuilder<IncidentKnowledge> builder)
    {
        
        // Relationships
        // Prevent same pair of knowledge and incident
        builder.HasIndex(ik => new { ik.IncidentId, ik.KnowledgeId })
            .IsUnique();
    }
}