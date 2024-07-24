using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Converters;

internal class OkxCopyTradingCopyModeConverter(bool quotes) : BaseConverter<OkxCopyTradingCopyMode>(quotes)
{
    public OkxCopyTradingCopyModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingCopyMode, string>> Mapping =>
    [
        new(OkxCopyTradingCopyMode.FixedAmount, "fixed_amount"),
        new(OkxCopyTradingCopyMode.RatioCopy, "ratio_copy"),
    ];
}