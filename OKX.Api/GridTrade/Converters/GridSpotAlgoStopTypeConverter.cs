using OKX.Api.GridTrade.Enums;

namespace OKX.Api.GridTrade.Converters;

internal class GridSpotAlgoStopTypeConverter : BaseConverter<OkxGridSpotAlgoStopType>
{
    public GridSpotAlgoStopTypeConverter() : this(true) { }
    public GridSpotAlgoStopTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridSpotAlgoStopType, string>> Mapping => new List<KeyValuePair<OkxGridSpotAlgoStopType, string>>
    {
        new KeyValuePair<OkxGridSpotAlgoStopType, string>(OkxGridSpotAlgoStopType.SellBaseCurrency, "1"),
        new KeyValuePair<OkxGridSpotAlgoStopType, string>(OkxGridSpotAlgoStopType.KeepBaseCurrency, "2"),
    };
}