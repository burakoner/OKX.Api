using OKX.Api.SignalBotTrading.Enums;

namespace OKX.Api.SignalBotTrading.Converters;

internal class OkxSignalSourceTypeConverter(bool quotes) : BaseConverter<OkxSignalSourceType>(quotes)
{
    public OkxSignalSourceTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalSourceType, string>> Mapping =>
    [
        new(OkxSignalSourceType.CreatedbyYourself, "1"),
        new(OkxSignalSourceType.Subscribe, "2"),
        new(OkxSignalSourceType.FreeSignal, "3"),
    ];
}