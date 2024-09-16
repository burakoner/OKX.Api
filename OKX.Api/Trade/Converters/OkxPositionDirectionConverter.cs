using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxPositionDirectionConverter(bool quotes) : BaseConverter<OkxPositionDirection>(quotes)
{
    public OkxPositionDirectionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPositionDirection, string>> Mapping =>
    [
        new(OkxPositionDirection.Long, "long"),
        new(OkxPositionDirection.Short, "short"),
    ];
}