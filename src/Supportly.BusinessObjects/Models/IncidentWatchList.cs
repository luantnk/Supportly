namespace Supportly.BusinessObjects.Models;

public class IncidentWatchList : BaseEntity
{
    // Properties
    public Guid IncidentId { get; set; }
    
    // Relationships
    public Incident Incident { get; set; } = null!;
}