using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoSubOrderTypeConverter : BaseConverter<OkxGridAlgoSubOrderType>
{
    public OkxGridAlgoSubOrderTypeConverter() : this(true) { }
    public OkxGridAlgoSubOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoSubOrderType, string>> Mapping =>
    [
        new(OkxGridAlgoSubOrderType.Live, "live"),
        new(OkxGridAlgoSubOrderType.Filled, "filled"),
    ];
}