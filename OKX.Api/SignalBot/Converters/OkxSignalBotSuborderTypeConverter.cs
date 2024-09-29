namespace OKX.Api.SignalBot;

internal class OkxSignalBotSuborderTypeConverter(bool quotes) : BaseConverter<OkxSignalBotSuborderType>(quotes)
{
    public OkxSignalBotSuborderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotSuborderType, string>> Mapping =>
    [
        new(OkxSignalBotSuborderType.Live, "live"),
        new(OkxSignalBotSuborderType.Filled, "filled"),
    ];
}