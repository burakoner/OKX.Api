using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

public class OkxPositionModeConverter(bool quotes) : BaseConverter<OkxPositionMode>(quotes)
{
    public OkxPositionModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPositionMode, string>> Mapping =>
    [
        new(OkxPositionMode.LongShortMode, "long_short_mode"),
        new(OkxPositionMode.NetMode, "net_mode"),
    ];
}