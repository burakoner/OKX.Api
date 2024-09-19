using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Converters;

internal class OkxCopyTradingSubPositionTypeConverter(bool quotes) : BaseConverter<OkxCopyTradingSubPositionType>(quotes)
{
    public OkxCopyTradingSubPositionTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingSubPositionType, string>> Mapping =>
    [
        new(OkxCopyTradingSubPositionType.Lead, "lead"),
        new(OkxCopyTradingSubPositionType.Copy, "copy"),
    ];
}