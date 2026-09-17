using Microsoft.AspNetCore.Identity;

namespace Supportly.BusinessObjects.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    // Relationships
    public ICollection<IncidentActivity> IncidentActivities { get; set; } = new List<IncidentActivity>();

    public ICollection<IncidentWatchList> IncidentWatchLists { get; set; }
        = new List<IncidentWatchList>();
}