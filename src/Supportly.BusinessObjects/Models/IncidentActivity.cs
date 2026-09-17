namespace Supportly.BusinessObjects.Models;

public class IncidentActivity : BaseEntity
{
    // Properties
    public Guid IncidentId { get; set; }

    public Guid UserId { get; set; }
    
    public string? ActivityType { get; set; }
    
    public string? Description { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }
    
    // Relationship
    public Incident Incident { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;
}