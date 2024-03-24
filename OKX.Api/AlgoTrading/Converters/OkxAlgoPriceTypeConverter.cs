using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

internal class OkxAlgoPriceTypeConverter : BaseConverter<OkxAlgoPriceType>
{
    public OkxAlgoPriceTypeConverter() : this(true) { }
    public OkxAlgoPriceTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAlgoPriceType, string>> Mapping =>
    [
        new(OkxAlgoPriceType.Last, "last"),
        new(OkxAlgoPriceType.Index, "index"),
        new(OkxAlgoPriceType.Mark, "mark"),
    ];
}