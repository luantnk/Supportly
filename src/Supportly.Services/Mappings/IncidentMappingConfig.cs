using Mapster;
using Supportly.BusinessObjects.Models;
using Supportly.Services.DTOs.Incidents;

namespace Supportly.Services.Mappings;

public static class IncidentMappingConfig
{
    public static void Register()
    {
        TypeAdapterConfig<CreateIncidentRequest, Incident>
            .NewConfig();

        TypeAdapterConfig<UpdateIncidentRequest, Incident>
            .NewConfig();

        TypeAdapterConfig<Incident, IncidentResponse>
            .NewConfig();

        TypeAdapterConfig<Incident, IncidentListItemResponse>
            .NewConfig();
    }
}