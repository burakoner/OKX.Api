using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxMaintenanceStateConverter(bool quotes) : BaseConverter<OkxMaintenanceState>(quotes)
{
    public OkxMaintenanceStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxMaintenanceState, string>> Mapping =>
    [
        new(OkxMaintenanceState.Scheduled, "scheduled"),
        new(OkxMaintenanceState.Ongoing, "ongoing"),
        new(OkxMaintenanceState.Completed, "completed"),
        new(OkxMaintenanceState.Canceled, "canceled"),
    ];
}