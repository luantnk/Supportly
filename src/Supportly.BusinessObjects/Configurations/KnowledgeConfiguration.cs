using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supportly.BusinessObjects.Models;

namespace Supportly.BusinessObjects.Configurations;

public class KnowledgeConfiguration : IEntityTypeConfiguration<Knowledge>
{
    public void Configure(EntityTypeBuilder<Knowledge> builder)
    {
        // Properties
        builder.Property(k => k.Number)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(k => k.Number)
            .IsUnique();

        builder.Property(k => k.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(k => k.ShortDescription)
            .HasMaxLength(500);

        // Relationships 
        builder.HasMany(k => k.IncidentKnowledges)
            .WithOne(ik => ik.Knowledge)
            .HasForeignKey(ik => ik.KnowledgeId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
    
}