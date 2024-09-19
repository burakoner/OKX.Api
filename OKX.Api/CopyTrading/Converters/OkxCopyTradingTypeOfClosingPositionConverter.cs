using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Converters;

internal class OkxCopyTradingTypeOfClosingPositionConverter(bool quotes) : BaseConverter<OkxCopyTradingTypeOfClosingPosition>(quotes)
{
    public OkxCopyTradingTypeOfClosingPositionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingTypeOfClosingPosition, string>> Mapping =>
    [
        new(OkxCopyTradingTypeOfClosingPosition.ClosePositionPartially, "1"),
        new(OkxCopyTradingTypeOfClosingPosition.CloseAll, "2"),
    ];
}