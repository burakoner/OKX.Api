using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxPositionSideConverter : BaseConverter<OkxPositionSide>
{
    public OkxPositionSideConverter() : this(true) { }
    public OkxPositionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxPositionSide, string>> Mapping =>
    [
        new(OkxPositionSide.Long, "long"),
        new(OkxPositionSide.Short, "short"),
        new(OkxPositionSide.Net, "net"),
    ];
}