using Mapster;
using Supportly.BusinessObjects.Enums;
using Supportly.BusinessObjects.Models;
using Supportly.Repositories.Interface;
using Supportly.Services.DTOs.Incidents;
using Supportly.Services.Interfaces;

namespace Supportly.Services.Implementations;

public class IncidentService(IUnitOfWork unitOfWork) : IIncidentService
{
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

    public async Task<IReadOnlyList<IncidentListItemResponse>> GetAssignedToAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var incidents = await unitOfWork.Incidents
            .GetAssignedToAsync(
                userId,
                cancellationToken);

        return incidents
            .Adapt<IReadOnlyList<IncidentListItemResponse>>();
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

        unitOfWork.Incidents.Remove(incident);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static string GenerateIncidentNumber()
    {
        return $"INC-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}