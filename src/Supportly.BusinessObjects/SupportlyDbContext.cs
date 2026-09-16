using Microsoft.AspNetCore.Identity;
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
    public DbSet<Incident> Incidents { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}