using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class GridAlgoOrderTypeConverter : BaseConverter<OkxGridAlgoOrderType>
{
    public GridAlgoOrderTypeConverter() : this(true) { }
    public GridAlgoOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoOrderType, string>> Mapping => new List<KeyValuePair<OkxGridAlgoOrderType, string>>
    {
        new KeyValuePair<OkxGridAlgoOrderType, string>(OkxGridAlgoOrderType.SpotGrid, "grid"),
        new KeyValuePair<OkxGridAlgoOrderType, string>(OkxGridAlgoOrderType.ContractGrid, "contract_grid"),
        new KeyValuePair<OkxGridAlgoOrderType, string>(OkxGridAlgoOrderType.MoonGrid, "moon_grid"),
    };
}