namespace OKX.Api.CopyTrading;

internal class OkxCopyTradingLeadingModeConverter(bool quotes) : BaseConverter<OkxCopyTradingLeadingMode>(quotes)
{
    public OkxCopyTradingLeadingModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingLeadingMode, string>> Mapping =>
    [
        new(OkxCopyTradingLeadingMode.Public, "public"),
        new(OkxCopyTradingLeadingMode.Private, "private"),
    ];
}