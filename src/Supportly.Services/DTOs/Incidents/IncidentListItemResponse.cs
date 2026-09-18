using Supportly.BusinessObjects.Enums;

namespace Supportly.Services.DTOs.Incidents;

public class IncidentListItemResponse
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public IncidentPriority? Priority { get; set; }

    public IncidentState? State { get; set; }

    public Guid? AssignedToId { get; set; }

    public DateTime CreatedAt { get; set; }
}