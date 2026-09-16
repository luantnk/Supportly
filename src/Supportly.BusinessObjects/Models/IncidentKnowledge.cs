namespace Supportly.BusinessObjects.Models;

public class IncidentKnowledge : BaseEntity
{
    public Guid IncidentId { get; set; }
    public Guid KnowledgeId { get; set; }

    public Incident Incident { get; set; } = null!;
    public Knowledge Knowledge { get; set; } = null!;
}