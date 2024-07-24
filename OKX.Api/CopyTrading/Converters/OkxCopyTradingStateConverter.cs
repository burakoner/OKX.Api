using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Converters;

internal class OkxCopyTradingStateConverter(bool quotes) : BaseConverter<OkxCopyTradingState>(quotes)
{
    public OkxCopyTradingStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingState, string>> Mapping =>
    [
        new(OkxCopyTradingState.NonCopy, "0"),
        new(OkxCopyTradingState.Copy, "1"),
    ];
}