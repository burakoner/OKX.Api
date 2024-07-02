using OKX.Api.SignalTrading.Enums;

namespace OKX.Api.SignalTrading.Converters;

internal class OkxSignalEntryTypeConverter(bool quotes) : BaseConverter<OkxSignalEntryType>(quotes)
{
    public OkxSignalEntryTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalEntryType, string>> Mapping =>
    [
        new(OkxSignalEntryType.TradingViewSignal, "1"),
        new(OkxSignalEntryType.FixedMargin, "2"),
        new(OkxSignalEntryType.Contracts, "3"),
        new(OkxSignalEntryType.PercentageOfFreeMargin, "4"),
        new(OkxSignalEntryType.PercentageOfTheInitialInvestedMargin, "5"),
    ];
}