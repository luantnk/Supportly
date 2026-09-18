using Supportly.BusinessObjects.Enums;

namespace Supportly.Services.DTOs.Incidents;

public class CreateIncidentRequest
{
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public IncidentPriority? Priority { get; set; }

    public Guid? ParentIncidentId { get; set; }

    public Guid? AssignedToId { get; set; }
}