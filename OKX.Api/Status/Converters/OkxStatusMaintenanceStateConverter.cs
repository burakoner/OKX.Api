using OKX.Api.Status.Enums;

namespace OKX.Api.Status.Converters;

internal class OkxStatusMaintenanceStateConverter(bool quotes) : BaseConverter<OkxStatusMaintenanceState>(quotes)
{
    public OkxStatusMaintenanceStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxStatusMaintenanceState, string>> Mapping =>
    [
        new(OkxStatusMaintenanceState.Scheduled, "scheduled"),
        new(OkxStatusMaintenanceState.Ongoing, "ongoing"),
        new(OkxStatusMaintenanceState.PreOpen, "pre_open"),
        new(OkxStatusMaintenanceState.Completed, "completed"),
        new(OkxStatusMaintenanceState.Canceled, "canceled"),
    ];
}