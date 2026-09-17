namespace Supportly.BusinessObjects.Models;

public class IncidentWatchList : BaseEntity
{
    // Properties
    public Guid IncidentId { get; set; }

    public Guid UserId { get; set; }
    
    // Relationships
    public Incident Incident { get; set; } = null!;
    
    public ApplicationUser User { get; set; } = null!;
    
    
}