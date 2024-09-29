namespace OKX.Api.Grid;

internal class OkxGridSpotAlgoStopTypeConverter(bool quotes) : BaseConverter<OkxGridSpotAlgoStopType>(quotes)
{
    public OkxGridSpotAlgoStopTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridSpotAlgoStopType, string>> Mapping =>
    [
        new(OkxGridSpotAlgoStopType.SellBaseCurrency, "1"),
        new(OkxGridSpotAlgoStopType.KeepBaseCurrency, "2"),
    ];
}