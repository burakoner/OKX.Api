using OKX.Api.GridTrade.Enums;

namespace OKX.Api.GridTrade.Converters;

internal class GridContractAlgoStopTypeConverter : BaseConverter<OkxGridContractAlgoStopType>
{
    public GridContractAlgoStopTypeConverter() : this(true) { }
    public GridContractAlgoStopTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridContractAlgoStopType, string>> Mapping => new List<KeyValuePair<OkxGridContractAlgoStopType, string>>
    {
        new KeyValuePair<OkxGridContractAlgoStopType, string>(OkxGridContractAlgoStopType.MarketCloseAllPositions, "1"),
        new KeyValuePair<OkxGridContractAlgoStopType, string>(OkxGridContractAlgoStopType.KeepPositions, "2"),
    };
}