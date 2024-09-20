using OKX.Api.Status.Enums;

namespace OKX.Api.Status.Converters;

internal class OkxStatusMaintenanceServiceConverter(bool quotes) : BaseConverter<OkxStatusMaintenanceService>(quotes)
{
    public OkxStatusMaintenanceServiceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxStatusMaintenanceService, string>> Mapping =>
    [
        new(OkxStatusMaintenanceService.WebSocket, "0"),
        //new(OkxMaintenanceService.SpotMargin, "1"),
        //new(OkxMaintenanceService.Futures, "2"),
        //new(OkxMaintenanceService.Perpetual, "3"),
        //new(OkxMaintenanceService.Options, "4"),
        new(OkxStatusMaintenanceService.TradingService, "5"),
        new(OkxStatusMaintenanceService.BlockTrading, "6"),
        new(OkxStatusMaintenanceService.TradingBot, "7"),
        new(OkxStatusMaintenanceService.TradingServiceInBatchesOfAccounts, "8"),
        new(OkxStatusMaintenanceService.TradingServiceInBatchesOfProducts, "9"),
        new(OkxStatusMaintenanceService.SpreadTrading, "10"),
        new(OkxStatusMaintenanceService.CopyTrading, "11"),
        new(OkxStatusMaintenanceService.Others, "99"),
    ];
}