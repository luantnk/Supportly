using Supportly.Services.DTOs.Common;
using Supportly.Services.DTOs.Incidents;

namespace Supportly.Services.Interfaces;

public interface IIncidentService
{
    Task<PagedResponse<IncidentListItemResponse>> GetAsync(
        IncidentQuery query,
        CancellationToken cancellationToken = default);

    Task<IncidentResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IncidentResponse?> GetByNumberAsync(
        string number,
        CancellationToken cancellationToken = default);

    Task<IncidentResponse> CreateAsync(
        CreateIncidentRequest request,
        CancellationToken cancellationToken = default);

    Task<IncidentResponse?> UpdateAsync(
        Guid id,
        UpdateIncidentRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}