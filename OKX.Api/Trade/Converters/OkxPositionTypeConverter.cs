using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxPositionTypeConverter(bool quotes) : BaseConverter<OkxPositionType>(quotes)
{
    public OkxPositionTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPositionType, string>> Mapping =>
    [
        new(OkxPositionType.ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstruments, "1"),
        new(OkxPositionType.ContractsOfPendingOrdersForAllDerivativesInstruments, "2"),
        new(OkxPositionType.PendingOrdersForAllDerivativesInstruments, "3"),
        new(OkxPositionType.ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstrumentsOnTheSameSide, "4"),
        new(OkxPositionType.PendingOrdersForOneDerivativesInstrument, "5"),
        new(OkxPositionType.ContractsOfPendingOrdersAndOpenPositionsForOneDerivativesInstrument, "6"),
        new(OkxPositionType.ContractsOfOnePendingOrder, "7"),
    ];
}