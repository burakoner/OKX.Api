using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoOrderTypeConverter : BaseConverter<OkxGridAlgoOrderType>
{
    public OkxGridAlgoOrderTypeConverter() : this(true) { }
    public OkxGridAlgoOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoOrderType, string>> Mapping =>
    [
        new(OkxGridAlgoOrderType.SpotGrid, "grid"),
        new(OkxGridAlgoOrderType.ContractGrid, "contract_grid"),
        new(OkxGridAlgoOrderType.MoonGrid, "moon_grid"),
    ];
}