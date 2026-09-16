namespace Supportly.BusinessObjects.Models;

public class Knowledge : BaseEntity
{
    // Properties
    public string Number { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    
    // Relationships
    public ICollection<IncidentKnowledge> IncidentKnowledge { get; set; } = new List<IncidentKnowledge>();
}