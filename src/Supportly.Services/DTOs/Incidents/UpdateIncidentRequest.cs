using Supportly.BusinessObjects.Enums;

namespace Supportly.Services.DTOs.Incidents;

public class UpdateIncidentRequest
{
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public IncidentPriority? Priority { get; set; }

    public IncidentState? State { get; set; }

    public string? ResolutionCode { get; set; }

    public string? ResolutionNotes { get; set; }

    public Guid? ParentIncidentId { get; set; }

    public Guid? AssignedToId { get; set; }
}