using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxMaintenanceServiceConverter(bool quotes) : BaseConverter<OkxMaintenanceService>(quotes)
{
    public OkxMaintenanceServiceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxMaintenanceService, string>> Mapping =>
    [
        new(OkxMaintenanceService.WebSocket, "0"),
        //new(OkxMaintenanceService.SpotMargin, "1"),
        //new(OkxMaintenanceService.Futures, "2"),
        //new(OkxMaintenanceService.Perpetual, "3"),
        //new(OkxMaintenanceService.Options, "4"),
        new(OkxMaintenanceService.TradingService, "5"),
        new(OkxMaintenanceService.BlockTrading, "6"),
        new(OkxMaintenanceService.TradingBot, "7"),
        new(OkxMaintenanceService.TradingServiceInBatchesOfAccounts, "8"),
        new(OkxMaintenanceService.TradingServiceInBatchesOfProducts, "9"),
        new(OkxMaintenanceService.SpreadTrading, "10"),
        new(OkxMaintenanceService.CopyTrading, "11"),
        new(OkxMaintenanceService.Others, "99"),
    ];
}