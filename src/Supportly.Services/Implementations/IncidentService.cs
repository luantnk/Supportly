using Mapster;
using Supportly.BusinessObjects.Enums;
using Supportly.BusinessObjects.Models;
using Supportly.Repositories.Interface;
using Supportly.Services.DTOs.Common;
using Supportly.Services.DTOs.Incidents;
using Supportly.Services.Interfaces;

namespace Supportly.Services.Implementations;

public class IncidentService(IUnitOfWork unitOfWork) : IIncidentService
{
    private const int DefaultPageSize = 20;
    private const int MaxPageSize = 100;

    public async Task<PagedResponse<IncidentListItemResponse>> GetAsync(
        IncidentQuery query,
        CancellationToken cancellationToken = default)
    {
        var page = Math.Max(query.Page, 1);
        var pageSize = Math.Clamp(
            query.PageSize <= 0 ? DefaultPageSize : query.PageSize,
            1,
            MaxPageSize);

        var (items, totalCount) = await unitOfWork.Incidents
            .GetPagedAsync<IncidentListItemResponse>(
                query.AssignedTo,
                query.Status,
                page,
                pageSize,
                cancellationToken);

        return new PagedResponse<IncidentListItemResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }

    public async Task<IncidentResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var incident = await unitOfWork.Incidents
            .GetWithDetailsAsync(
                id,
                asNoTracking: true,
                cancellationToken);

        return incident?.Adapt<IncidentResponse>();
    }

    public async Task<IncidentResponse?> GetByNumberAsync(
        string number,
        CancellationToken cancellationToken = default)
    {
        var incident = await unitOfWork.Incidents
            .GetByNumberAsync(
                number,
                cancellationToken);

        return incident?.Adapt<IncidentResponse>();
    }

    public async Task<IncidentResponse> CreateAsync(
        CreateIncidentRequest request,
        CancellationToken cancellationToken = default)
    {
        var incident = request.Adapt<Incident>();

        // Business logic belongs here
        incident.Number = GenerateIncidentNumber();
        incident.State = IncidentState.New;

        unitOfWork.Incidents.Add(incident);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return incident.Adapt<IncidentResponse>();
    }

    public async Task<IncidentResponse?> UpdateAsync(
        Guid id,
        UpdateIncidentRequest request,
        CancellationToken cancellationToken = default)
    {
        var incident = await unitOfWork.Incidents
            .GetByIdAsync(id, cancellationToken);

        if (incident is null)
            return null;

        // Map DTO → existing entity
        request.Adapt(incident);

        unitOfWork.Incidents.Update(incident);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return incident.Adapt<IncidentResponse>();
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var incident = await unitOfWork.Incidents
            .GetByIdAsync(id, cancellationToken);

        if (incident is null)
            return false;

        incident.IsDeleted = true;
        incident.DeletedAt = DateTime.UtcNow;

        unitOfWork.Incidents.Update(incident);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static string GenerateIncidentNumber()
    {
        return $"INC-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}