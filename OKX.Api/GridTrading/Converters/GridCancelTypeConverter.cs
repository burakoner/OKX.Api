using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class GridCancelTypeConverter : BaseConverter<OkxGridCancelType>
{
    public GridCancelTypeConverter() : this(true) { }
    public GridCancelTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridCancelType, string>> Mapping => new List<KeyValuePair<OkxGridCancelType, string>>
    {
        new KeyValuePair<OkxGridCancelType, string>(OkxGridCancelType.None, "0"),
        new KeyValuePair<OkxGridCancelType, string>(OkxGridCancelType.ManualStop, "1"),
        new KeyValuePair<OkxGridCancelType, string>(OkxGridCancelType.TakeProfit, "2"),
        new KeyValuePair<OkxGridCancelType, string>(OkxGridCancelType.StopLoss, "3"),
        new KeyValuePair<OkxGridCancelType, string>(OkxGridCancelType.RiskControl, "4"),
        new KeyValuePair<OkxGridCancelType, string>(OkxGridCancelType.Delivery, "5"),
        new KeyValuePair<OkxGridCancelType, string>(OkxGridCancelType.Signal, "5"),
    };
}