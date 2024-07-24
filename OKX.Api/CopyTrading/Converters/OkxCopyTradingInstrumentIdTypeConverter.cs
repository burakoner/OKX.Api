using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Converters;

internal class OkxCopyTradingInstrumentIdTypeConverter(bool quotes) : BaseConverter<OkxCopyTradingInstrumentIdType>(quotes)
{
    public OkxCopyTradingInstrumentIdTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingInstrumentIdType, string>> Mapping =>
    [
        new(OkxCopyTradingInstrumentIdType.Custom, "custom"),
        new(OkxCopyTradingInstrumentIdType.Copy, "copy"),
    ];
}