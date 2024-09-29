namespace OKX.Api.SignalBot;

internal class OkxSignalBotEventStatusConverter(bool quotes) : BaseConverter<OkxSignalBotEventStatus>(quotes)
{
    public OkxSignalBotEventStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotEventStatus, string>> Mapping =>
    [
        new(OkxSignalBotEventStatus.Success, "success"),
        new(OkxSignalBotEventStatus.Failure, "failure"),
    ];
}