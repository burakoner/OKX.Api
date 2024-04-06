using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

public class OkxGridContractAlgoStopTypeConverter(bool quotes) : BaseConverter<OkxGridContractAlgoStopType>(quotes)
{
    public OkxGridContractAlgoStopTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridContractAlgoStopType, string>> Mapping =>
    [
        new(OkxGridContractAlgoStopType.MarketCloseAllPositions, "1"),
        new(OkxGridContractAlgoStopType.KeepPositions, "2"),
    ];
}