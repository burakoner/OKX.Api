namespace OKX.Api.SignalBot;

internal class OkxSignalBotSuborderStateConverter(bool quotes) : BaseConverter<OkxSignalBotSuborderState>(quotes)
{
    public OkxSignalBotSuborderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotSuborderState, string>> Mapping =>
    [
        new(OkxSignalBotSuborderState.Live, "live"),
        new(OkxSignalBotSuborderState.PartiallyFilled, "partially_filled"),
        new(OkxSignalBotSuborderState.Filled, "filled"),
        new(OkxSignalBotSuborderState.Cancelling, "cancelling"),
        new(OkxSignalBotSuborderState.Cancelled, "cancelled"),
    ];
}