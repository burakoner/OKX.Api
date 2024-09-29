namespace OKX.Api.Trade;

internal class OkxTradePositionTypeConverter(bool quotes) : BaseConverter<OkxTradePositionType>(quotes)
{
    public OkxTradePositionTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradePositionType, string>> Mapping =>
    [
        new(OkxTradePositionType.ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstruments, "1"),
        new(OkxTradePositionType.ContractsOfPendingOrdersForAllDerivativesInstruments, "2"),
        new(OkxTradePositionType.PendingOrdersForAllDerivativesInstruments, "3"),
        new(OkxTradePositionType.ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstrumentsOnTheSameSide, "4"),
        new(OkxTradePositionType.PendingOrdersForOneDerivativesInstrument, "5"),
        new(OkxTradePositionType.ContractsOfPendingOrdersAndOpenPositionsForOneDerivativesInstrument, "6"),
        new(OkxTradePositionType.ContractsOfOnePendingOrder, "7"),
    ];
}