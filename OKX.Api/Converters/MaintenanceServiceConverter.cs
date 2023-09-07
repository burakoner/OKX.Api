namespace OKX.Api.Converters;

internal class MaintenanceServiceConverter : BaseConverter<OkxMaintenanceService>
{
    public MaintenanceServiceConverter() : this(true) { }
    public MaintenanceServiceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMaintenanceService, string>> Mapping => new List<KeyValuePair<OkxMaintenanceService, string>>
    {
        new KeyValuePair<OkxMaintenanceService, string>(OkxMaintenanceService.WebSocket, "0"),
        new KeyValuePair<OkxMaintenanceService, string>(OkxMaintenanceService.SpotMargin, "1"),
        new KeyValuePair<OkxMaintenanceService, string>(OkxMaintenanceService.Futures, "2"),
        new KeyValuePair<OkxMaintenanceService, string>(OkxMaintenanceService.Perpetual, "3"),
        new KeyValuePair<OkxMaintenanceService, string>(OkxMaintenanceService.Options, "4"),
        new KeyValuePair<OkxMaintenanceService, string>(OkxMaintenanceService.Trading, "5"),
    };
}