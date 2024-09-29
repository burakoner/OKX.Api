namespace OKX.Api.SignalBot;

internal class OkxSignalBotOrderStateConverter(bool quotes) : BaseConverter<OkxSignalBotOrderState>(quotes)
{
    public OkxSignalBotOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotOrderState, string>> Mapping =>
    [
        new(OkxSignalBotOrderState.Starting, "starting"),
        new(OkxSignalBotOrderState.Running, "running"),
        new(OkxSignalBotOrderState.Stopping, "stopping"),
        new(OkxSignalBotOrderState.Stopped, "stopped"),
    ];
}