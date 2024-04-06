using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxMaintenanceStateConverter(bool quotes) : BaseConverter<OkxMaintenanceState>(quotes)
{
    public OkxMaintenanceStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxMaintenanceState, string>> Mapping =>
    [
        new(OkxMaintenanceState.Scheduled, "scheduled"),
        new(OkxMaintenanceState.Ongoing, "ongoing"),
        new(OkxMaintenanceState.PreOpen, "pre_open"),
        new(OkxMaintenanceState.Completed, "completed"),
        new(OkxMaintenanceState.Canceled, "canceled"),
    ];
}