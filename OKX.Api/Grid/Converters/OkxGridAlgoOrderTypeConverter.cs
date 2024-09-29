namespace OKX.Api.Grid;

internal class OkxGridAlgoOrderTypeConverter(bool quotes) : BaseConverter<OkxGridAlgoOrderType>(quotes)
{
    public OkxGridAlgoOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoOrderType, string>> Mapping =>
    [
        new(OkxGridAlgoOrderType.SpotGrid, "grid"),
        new(OkxGridAlgoOrderType.ContractGrid, "contract_grid"),
    ];
}