using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class GridAlgoSubOrderTypeConverter : BaseConverter<OkxGridAlgoSubOrderType>
{
    public GridAlgoSubOrderTypeConverter() : this(true) { }
    public GridAlgoSubOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoSubOrderType, string>> Mapping => new List<KeyValuePair<OkxGridAlgoSubOrderType, string>>
    {
        new KeyValuePair<OkxGridAlgoSubOrderType, string>(OkxGridAlgoSubOrderType.Live, "live"),
        new KeyValuePair<OkxGridAlgoSubOrderType, string>(OkxGridAlgoSubOrderType.Filled, "filled"),
    };
}