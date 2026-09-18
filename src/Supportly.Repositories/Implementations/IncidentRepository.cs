using Microsoft.EntityFrameworkCore;
using Supportly.BusinessObjects;
using Supportly.BusinessObjects.Models;
using Supportly.Repositories.Interface;

namespace Supportly.Repositories.Implementation;

public class IncidentRepository : GenericRepository<Incident>, IIncidentRepository
{
    public IncidentRepository(SupportlyDbContext context) : base(context)
    {
    }

    public async Task<Incident?> GetByNumberAsync(
        string number,
        CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(i => i.Number == number, cancellationToken);

    public async Task<Incident?> GetWithDetailsAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Incident> query = _dbSet
            .Include(i => i.AssignedTo)
            .Include(i => i.ResolvedBy)
            .Include(i => i.ParentIncident)
            .Include(i => i.IncidentActivities)
            .Include(i => i.IncidentJournals)
            .Include(i => i.IncidentKnowledges)
            .Include(i => i.IncidentWatchLists)
            .AsSplitQuery(); // avoids a cartesian explosion from 4 collection includes

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Incident>> GetAssignedToAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .Where(i => i.AssignedToId == userId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);
}