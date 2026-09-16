using Supportly.BusinessObjects.Enums;

namespace Supportly.BusinessObjects.Models;

public class IncidentJournal : BaseEntity
{
    // Properties
    public Guid IncidentId { get; set; }
    public IncidentJournalType Type { get; set; }
    public string? Content { get; set; }
    
    
    // Relationships
    public Incident Incident { get; set; } = null!;
}