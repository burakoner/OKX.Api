using OKX.Api.SignalBot.Enums;

namespace OKX.Api.SignalBot.Converters;

internal class OkxSignalBotEventStatusConverter(bool quotes) : BaseConverter<OkxSignalBotEventStatus>(quotes)
{
    public OkxSignalBotEventStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotEventStatus, string>> Mapping =>
    [
        new(OkxSignalBotEventStatus.Success, "success"),
        new(OkxSignalBotEventStatus.Failure, "failure"),
    ];
}