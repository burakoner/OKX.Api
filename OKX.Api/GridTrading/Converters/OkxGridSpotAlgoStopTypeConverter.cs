using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridSpotAlgoStopTypeConverter : BaseConverter<OkxGridSpotAlgoStopType>
{
    public OkxGridSpotAlgoStopTypeConverter() : this(true) { }
    public OkxGridSpotAlgoStopTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridSpotAlgoStopType, string>> Mapping =>
    [
        new(OkxGridSpotAlgoStopType.SellBaseCurrency, "1"),
        new(OkxGridSpotAlgoStopType.KeepBaseCurrency, "2"),
    ];
}