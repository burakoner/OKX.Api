namespace OKX.Api.Converters;

internal class MaintenanceStateConverter : BaseConverter<OkxMaintenanceState>
{
    public MaintenanceStateConverter() : this(true) { }
    public MaintenanceStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMaintenanceState, string>> Mapping => new List<KeyValuePair<OkxMaintenanceState, string>>
    {
        new KeyValuePair<OkxMaintenanceState, string>(OkxMaintenanceState.Scheduled, "scheduled"),
        new KeyValuePair<OkxMaintenanceState, string>(OkxMaintenanceState.Ongoing, "ongoing"),
        new KeyValuePair<OkxMaintenanceState, string>(OkxMaintenanceState.Completed, "completed"),
        new KeyValuePair<OkxMaintenanceState, string>(OkxMaintenanceState.Canceled, "canceled"),
    };
}