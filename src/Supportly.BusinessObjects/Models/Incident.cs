using System.ComponentModel.DataAnnotations;
using Supportly.BusinessObjects.Enums;

namespace Supportly.BusinessObjects.Models;

public class Incident : BaseEntity
{
    // Properties
    public required string Number { get; set; }

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public IncidentPriority? Priority { get; set; }

    public IncidentState? State { get; set; }

    public string? ResolutionCode { get; set; }

    public string? ResolutionNotes { get; set; }

    public Guid? ParentIncidentId { get; set; }
    
    // Relationships

    public Incident? ParentIncident { get; set; }

    public Guid? AssignedToId { get; set; }

    public ApplicationUser? AssignedTo { get; set; }

    public Guid? ResolvedById { get; set; }

    public ApplicationUser? ResolvedBy { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public ICollection<IncidentActivity> IncidentActivities { get; set; } = new List<IncidentActivity>();
    
    public ICollection<IncidentJournal> IncidentJournals  { get; set; } = new List<IncidentJournal>();
    
    public ICollection<IncidentKnowledge> IncidentKnowledges { get; set; } = new List<IncidentKnowledge>();
    
    public ICollection<IncidentWatchList>  IncidentWatchLists { get; set; } = new List<IncidentWatchList>();
}