using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridRunTypeConverter(bool quotes) : BaseConverter<OkxGridRunType>(quotes)
{
    public OkxGridRunTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridRunType, string>> Mapping =>
    [
        new(OkxGridRunType.Arithmetic, "1"),
        new(OkxGridRunType.Geometric, "2"),
    ];
}