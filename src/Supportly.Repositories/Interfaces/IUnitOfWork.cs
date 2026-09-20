using Supportly.BusinessObjects.Models;
using Supportly.Repositories.Interfaces;

namespace Supportly.Repositories.Interface;

public interface IUnitOfWork
{
    // Specific repositories: add one property per entity that needs custom queries.
    IIncidentRepository Incidents { get; }

    /// <summary>
    /// Generic repository for any entity that doesn't need a specific repository yet.
    /// </summary>
    IGenericRepository<T> Repository<T>() where T : BaseEntity;

    /// <returns>Number of rows affected.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    // Only needed when several SaveChanges calls must succeed or fail together.
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}