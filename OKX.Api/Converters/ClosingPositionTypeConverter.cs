namespace OKX.Api.Converters;

internal class ClosingPositionTypeConverter : BaseConverter<OkxClosingPositionType>
{
    public ClosingPositionTypeConverter() : this(true) { }
    public ClosingPositionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxClosingPositionType, string>> Mapping => new List<KeyValuePair<OkxClosingPositionType, string>>
        {
            new KeyValuePair<OkxClosingPositionType, string>(OkxClosingPositionType.ClosePartially, "1"),
            new KeyValuePair<OkxClosingPositionType, string>(OkxClosingPositionType.CloseAll, "2"),
            new KeyValuePair<OkxClosingPositionType, string>(OkxClosingPositionType.Liquidation, "3"),
            new KeyValuePair<OkxClosingPositionType, string>(OkxClosingPositionType.PartialLiquidation, "4"),
            new KeyValuePair<OkxClosingPositionType, string>(OkxClosingPositionType.ADL, "5"),
        };
}