namespace OKX.Api.SignalBot;

internal class OkxSignalBotEntryTypeConverter(bool quotes) : BaseConverter<OkxSignalBotEntryType>(quotes)
{
    public OkxSignalBotEntryTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotEntryType, string>> Mapping =>
    [
        new(OkxSignalBotEntryType.TradingViewSignal, "1"),
        new(OkxSignalBotEntryType.FixedMargin, "2"),
        new(OkxSignalBotEntryType.Contracts, "3"),
        new(OkxSignalBotEntryType.PercentageOfFreeMargin, "4"),
        new(OkxSignalBotEntryType.PercentageOfTheInitialInvestedMargin, "5"),
    ];
}