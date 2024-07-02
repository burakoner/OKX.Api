using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxPositionSideConverter(bool quotes) : BaseConverter<OkxPositionSide>(quotes)
{
    public OkxPositionSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPositionSide, string>> Mapping =>
    [
        new(OkxPositionSide.Long, "long"),
        new(OkxPositionSide.Short, "short"),
        new(OkxPositionSide.Net, "net"),
    ];
}