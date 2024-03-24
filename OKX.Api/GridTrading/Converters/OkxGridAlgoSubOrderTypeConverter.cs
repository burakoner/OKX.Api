using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoSubOrderTypeConverter(bool quotes) : BaseConverter<OkxGridAlgoSubOrderType>(quotes)
{
    public OkxGridAlgoSubOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoSubOrderType, string>> Mapping =>
    [
        new(OkxGridAlgoSubOrderType.Live, "live"),
        new(OkxGridAlgoSubOrderType.Filled, "filled"),
    ];
}