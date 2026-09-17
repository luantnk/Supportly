namespace Supportly.BusinessObjects.Models;

public class Knowledge : BaseEntity
{
    // Properties
    public required string Number { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }

    // Relationships
    public ICollection<IncidentKnowledge> IncidentKnowledges { get; set; } = new List<IncidentKnowledge>();
}