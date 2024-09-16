using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxInstrumentRuleTypeConverter(bool quotes) : BaseConverter<OkxInstrumentRuleType>(quotes)
{
    public OkxInstrumentRuleTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxInstrumentRuleType, string>> Mapping =>
    [
        new(OkxInstrumentRuleType.Normal, "normal"),
        new(OkxInstrumentRuleType.PreMarket, "pre_market"),
    ];
}