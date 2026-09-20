using Supportly.BusinessObjects.Enums;
using Supportly.BusinessObjects.Models;
using Supportly.Repositories.Interface;

namespace Supportly.Repositories.Interfaces;

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

    /// <summary>
    /// Returns a page of incidents, optionally filtered by assignee and status, ordered by
    /// <see cref="Incident.CreatedAt"/> descending. Projects directly to <typeparamref name="TResult"/>
    /// so only the required columns are read from the database.
    /// </summary>
    Task<(IReadOnlyList<TResult> Items, int TotalCount)> GetPagedAsync<TResult>(
        Guid? assignedToId,
        IncidentState? state,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}