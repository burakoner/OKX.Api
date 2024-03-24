using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridContractAlgoStopTypeConverter : BaseConverter<OkxGridContractAlgoStopType>
{
    public OkxGridContractAlgoStopTypeConverter() : this(true) { }
    public OkxGridContractAlgoStopTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridContractAlgoStopType, string>> Mapping =>
    [
        new(OkxGridContractAlgoStopType.MarketCloseAllPositions, "1"),
        new(OkxGridContractAlgoStopType.KeepPositions, "2"),
    ];
}