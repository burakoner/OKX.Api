namespace OKX.Api.CopyTrading;

internal class OkxCopyTradingMarginModeConverter(bool quotes) : BaseConverter<OkxCopyTradingMarginMode>(quotes)
{
    public OkxCopyTradingMarginModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingMarginMode, string>> Mapping =>
    [
        new(OkxCopyTradingMarginMode.Cross, "cross"),
        new(OkxCopyTradingMarginMode.Isolated, "isolated"),
        new(OkxCopyTradingMarginMode.Copy, "copy"),
    ];
}