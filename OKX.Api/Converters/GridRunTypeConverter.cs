namespace OKX.Api.Converters;

internal class GridRunTypeConverter : BaseConverter<OkxGridRunType>
{
    public GridRunTypeConverter() : this(true) { }
    public GridRunTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridRunType, string>> Mapping => new List<KeyValuePair<OkxGridRunType, string>>
    {
        new KeyValuePair<OkxGridRunType, string>(OkxGridRunType.Arithmetic, "1"),
        new KeyValuePair<OkxGridRunType, string>(OkxGridRunType.Geometric, "2"),
    };
}