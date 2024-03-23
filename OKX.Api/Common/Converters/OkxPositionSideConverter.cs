using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxPositionSideConverter : BaseConverter<OkxPositionSide>
{
    public OkxPositionSideConverter() : this(true) { }
    public OkxPositionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxPositionSide, string>> Mapping => new List<KeyValuePair<OkxPositionSide, string>>
    {
        new KeyValuePair<OkxPositionSide, string>(OkxPositionSide.Long, "long"),
        new KeyValuePair<OkxPositionSide, string>(OkxPositionSide.Short, "short"),
        new KeyValuePair<OkxPositionSide, string>(OkxPositionSide.Net, "net"),
    };
}