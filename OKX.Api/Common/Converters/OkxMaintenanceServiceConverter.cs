namespace OKX.Api.Common.Converters;

internal class OkxMaintenanceServiceConverter : BaseConverter<OkxMaintenanceService>
{
    public OkxMaintenanceServiceConverter() : this(true) { }
    public OkxMaintenanceServiceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMaintenanceService, string>> Mapping =>
    [
        new(OkxMaintenanceService.WebSocket, "0"),
        new(OkxMaintenanceService.SpotMargin, "1"),
        new(OkxMaintenanceService.Futures, "2"),
        new(OkxMaintenanceService.Perpetual, "3"),
        new(OkxMaintenanceService.Options, "4"),
        new(OkxMaintenanceService.Trading, "5"),
    ];
}