using OKX.Api.SignalBot.Enums;

namespace OKX.Api.SignalBot.Converters;

internal class OkxSignalBotEventTypeConverter(bool quotes) : BaseConverter<OkxSignalBotEventType>(quotes)
{
    public OkxSignalBotEventTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotEventType, string>> Mapping =>
    [
        new(OkxSignalBotEventType.SystemAction, "system_action"),
        new(OkxSignalBotEventType.UserAction, "user_action"),
        new(OkxSignalBotEventType.SignalProcessing, "signal_processing"),
    ];
}