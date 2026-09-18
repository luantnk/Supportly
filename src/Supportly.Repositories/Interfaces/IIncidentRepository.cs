using Supportly.BusinessObjects.Models;

namespace Supportly.Repositories.Interface;

public interface IIncidentRepository : IGenericRepository<Incident>
{
    Task<Incident?> GetByNumberAsync(
        string number,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Loads the incident with users, parent incident and all child collections.
    /// </summary>
    Task<Incident?> GetWithDetailsAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Incident>> GetAssignedToAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}