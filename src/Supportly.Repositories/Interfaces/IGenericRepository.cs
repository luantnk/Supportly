using System.Linq.Expressions;
using Supportly.BusinessObjects.Models;

namespace Supportly.Repositories.Interface;

public interface IGenericRepository<T> where T : BaseEntity
{

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <param name="include">
    /// Lets the caller shape the query, e.g.
    /// <c>q =&gt; q.Include(i =&gt; i.AssignedTo).Include(i =&gt; i.IncidentActivities)</c>
    /// (ThenInclude works too).
    /// </param>
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Paged query. <paramref name="orderBy"/> is required because paging
    /// without a deterministic order returns inconsistent pages.
    /// </summary>
    Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default);
    

    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}