using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Supportly.BusinessObjects.Models;

namespace Supportly.BusinessObjects;

public class SupportlyDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public SupportlyDbContext(DbContextOptions<SupportlyDbContext> options)
        : base(options)
    {
    }
    
    // Entities
    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<IncidentActivity> IncidentActivities { get; set; }

    public virtual DbSet<IncidentJournal> IncidentJournals { get; set; }

    public virtual DbSet<IncidentKnowledge> IncidentKnowledges { get; set; }

    public virtual DbSet<IncidentWatchList> IncidentWatchLists { get; set; }

    public virtual DbSet<Knowledge> Knowledges { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ConfigureAuditRelationships(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(SupportlyDbContext).Assembly);
    }

    private static void ConfigureAuditRelationships(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes()) 
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                continue;
            
            builder.Entity(entityType.ClrType)
                .HasOne(nameof(BaseEntity.CreatedBy))
                .WithMany()
                .HasForeignKey(nameof(BaseEntity.CreatedById))
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Entity(entityType.ClrType)
                .HasOne(nameof(BaseEntity.UpdatedBy))
                .WithMany()
                .HasForeignKey(nameof(BaseEntity.UpdatedById))
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}