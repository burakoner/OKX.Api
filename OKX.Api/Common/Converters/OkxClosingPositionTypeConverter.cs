namespace OKX.Api.Common.Converters;

internal class OkxClosingPositionTypeConverter : BaseConverter<OkxClosingPositionType>
{
    public OkxClosingPositionTypeConverter() : this(true) { }
    public OkxClosingPositionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxClosingPositionType, string>> Mapping =>
        [
            new(OkxClosingPositionType.ClosePartially, "1"),
            new(OkxClosingPositionType.CloseAll, "2"),
            new(OkxClosingPositionType.Liquidation, "3"),
            new(OkxClosingPositionType.PartialLiquidation, "4"),
            new(OkxClosingPositionType.ADL, "5"),
        ];
}