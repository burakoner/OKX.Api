namespace OKX.Api.CopyTrade;

internal class OkxCopyTradingPositionTypeConverter(bool quotes) : BaseConverter<OkxCopyTradingPositionType>(quotes)
{
    public OkxCopyTradingPositionTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingPositionType, string>> Mapping =>
    [
        new(OkxCopyTradingPositionType.Lead, "lead"),
        new(OkxCopyTradingPositionType.Copy, "copy"),
    ];
}