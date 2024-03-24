using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridCancelTypeConverter : BaseConverter<OkxGridCancelType>
{
    public OkxGridCancelTypeConverter() : this(true) { }
    public OkxGridCancelTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridCancelType, string>> Mapping =>
    [
        new(OkxGridCancelType.None, "0"),
        new(OkxGridCancelType.ManualStop, "1"),
        new(OkxGridCancelType.TakeProfit, "2"),
        new(OkxGridCancelType.StopLoss, "3"),
        new(OkxGridCancelType.RiskControl, "4"),
        new(OkxGridCancelType.Delivery, "5"),
        new(OkxGridCancelType.Signal, "5"),
    ];
}