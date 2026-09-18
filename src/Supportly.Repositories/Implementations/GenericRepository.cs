using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Supportly.BusinessObjects;
using Supportly.BusinessObjects.Models;
using Supportly.Repositories.Interface;

namespace Supportly.Repositories.Implementation;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly SupportlyDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(SupportlyDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object?[] { id }, cancellationToken);

    public virtual async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(filter, include, asNoTracking);

        if (orderBy is not null)
            query = orderBy(query);

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => await BuildQuery(filter, include, asNoTracking).FirstOrDefaultAsync(cancellationToken);

    public virtual async Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        pageNumber = Math.Max(pageNumber, 1);
        pageSize = Math.Max(pageSize, 1);

        var filtered = BuildQuery(filter, include: null, asNoTracking);
        var totalCount = await filtered.CountAsync(cancellationToken);

        var query = include is null ? filtered : include(filtered);

        var items = await orderBy(query)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public virtual async Task<bool> AnyAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(filter, cancellationToken);

    public virtual async Task<int> CountAsync(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default)
        => filter is null
            ? await _dbSet.CountAsync(cancellationToken)
            : await _dbSet.CountAsync(filter, cancellationToken);
    
    public virtual void Add(T entity) => _dbSet.Add(entity);

    public virtual void AddRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);

    public virtual void Update(T entity) => _dbSet.Update(entity);

    public virtual void Remove(T entity) => _dbSet.Remove(entity);

    public virtual void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
    

    protected IQueryable<T> BuildQuery(
        Expression<Func<T, bool>>? filter,
        Func<IQueryable<T>, IQueryable<T>>? include,
        bool asNoTracking)
    {
        IQueryable<T> query = _dbSet;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        if (filter is not null)
            query = query.Where(filter);

        return query;
    }
}