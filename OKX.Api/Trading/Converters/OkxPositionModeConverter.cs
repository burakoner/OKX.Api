using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Converters;

internal class OkxPositionModeConverter(bool quotes) : BaseConverter<OkxPositionMode>(quotes)
{
    public OkxPositionModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPositionMode, string>> Mapping =>
    [
        new(OkxPositionMode.LongShortMode, "long_short_mode"),
        new(OkxPositionMode.NetMode, "net_mode"),
    ];
}