using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

public class OkxGridAlgoOrderTypeConverter(bool quotes) : BaseConverter<OkxGridAlgoOrderType>(quotes)
{
    public OkxGridAlgoOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoOrderType, string>> Mapping =>
    [
        new(OkxGridAlgoOrderType.SpotGrid, "grid"),
        new(OkxGridAlgoOrderType.ContractGrid, "contract_grid"),
        new(OkxGridAlgoOrderType.MoonGrid, "moon_grid"),
    ];
}