namespace OKX.Api.CopyTrade;

internal class OkxCopyTradingPositionCloseTypeConverter(bool quotes) : BaseConverter<OkxCopyTradingPositionCloseType>(quotes)
{
    public OkxCopyTradingPositionCloseTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingPositionCloseType, string>> Mapping =>
    [
        new(OkxCopyTradingPositionCloseType.MarketClose, "market_close"),
        new(OkxCopyTradingPositionCloseType.CopyClose, "copy_close"),
        new(OkxCopyTradingPositionCloseType.ManualClose, "manual_close"),
    ];
}