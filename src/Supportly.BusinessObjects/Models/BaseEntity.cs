using System.ComponentModel.DataAnnotations;

namespace Supportly.BusinessObjects.Models;

public abstract class BaseEntity
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime  UpdatedAt { get; set; }

    public Guid CreatedById { get; set; }

    public required ApplicationUser CreatedBy { get; set; }

    public Guid? UpdatedById { get; set; }

    public ApplicationUser? UpdatedBy { get; set; }

    public bool IsActive { get; set; } = true;
}