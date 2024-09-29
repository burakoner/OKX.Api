namespace OKX.Api.CopyTrade;

internal class OkxCopyTradingModeConverter(bool quotes) : BaseConverter<OkxCopyTradingMode>(quotes)
{
    public OkxCopyTradingModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingMode, string>> Mapping =>
    [
        new(OkxCopyTradingMode.FixedAmount, "fixed_amount"),
        new(OkxCopyTradingMode.RatioCopy, "ratio_copy"),
    ];
}