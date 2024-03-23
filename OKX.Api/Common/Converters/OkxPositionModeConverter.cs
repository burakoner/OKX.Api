using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxPositionModeConverter : BaseConverter<OkxPositionMode>
{
    public OkxPositionModeConverter() : this(true) { }
    public OkxPositionModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxPositionMode, string>> Mapping =>
    [
        new(OkxPositionMode.LongShortMode, "long_short_mode"),
        new(OkxPositionMode.NetMode, "net_mode"),
    ];
}