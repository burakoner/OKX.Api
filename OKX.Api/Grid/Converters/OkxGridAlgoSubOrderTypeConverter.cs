namespace OKX.Api.Grid;

internal class OkxGridAlgoSubOrderTypeConverter(bool quotes) : BaseConverter<OkxGridAlgoSubOrderType>(quotes)
{
    public OkxGridAlgoSubOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoSubOrderType, string>> Mapping =>
    [
        new(OkxGridAlgoSubOrderType.Live, "live"),
        new(OkxGridAlgoSubOrderType.Filled, "filled"),
    ];
}