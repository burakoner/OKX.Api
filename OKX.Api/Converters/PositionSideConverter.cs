namespace OKX.Api.Converters;

internal class PositionSideConverter : BaseConverter<OkxPositionSide>
{
    public PositionSideConverter() : this(true) { }
    public PositionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxPositionSide, string>> Mapping => new List<KeyValuePair<OkxPositionSide, string>>
    {
        new KeyValuePair<OkxPositionSide, string>(OkxPositionSide.Long, "long"),
        new KeyValuePair<OkxPositionSide, string>(OkxPositionSide.Short, "short"),
        new KeyValuePair<OkxPositionSide, string>(OkxPositionSide.Net, "net"),
    };
}