using OKX.Api.SignalBot.Enums;

namespace OKX.Api.SignalBot.Converters;

internal class OkxSignalBotSourceTypeConverter(bool quotes) : BaseConverter<OkxSignalBotSourceType>(quotes)
{
    public OkxSignalBotSourceTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotSourceType, string>> Mapping =>
    [
        new(OkxSignalBotSourceType.CreatedbyYourself, "1"),
        new(OkxSignalBotSourceType.Subscribe, "2"),
        new(OkxSignalBotSourceType.FreeSignal, "3"),
    ];
}