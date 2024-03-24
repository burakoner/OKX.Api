namespace OKX.Api.Common.Converters;

internal class OkxMaintenanceStateConverter : BaseConverter<OkxMaintenanceState>
{
    public OkxMaintenanceStateConverter() : this(true) { }
    public OkxMaintenanceStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMaintenanceState, string>> Mapping =>
    [
        new(OkxMaintenanceState.Scheduled, "scheduled"),
        new(OkxMaintenanceState.Ongoing, "ongoing"),
        new(OkxMaintenanceState.Completed, "completed"),
        new(OkxMaintenanceState.Canceled, "canceled"),
    ];
}