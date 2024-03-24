using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridRunTypeConverter : BaseConverter<OkxGridRunType>
{
    public OkxGridRunTypeConverter() : this(true) { }
    public OkxGridRunTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridRunType, string>> Mapping =>
    [
        new(OkxGridRunType.Arithmetic, "1"),
        new(OkxGridRunType.Geometric, "2"),
    ];
}