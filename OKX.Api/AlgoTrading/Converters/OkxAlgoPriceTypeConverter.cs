using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

public class OkxAlgoPriceTypeConverter(bool quotes) : BaseConverter<OkxAlgoPriceType>(quotes)
{
    public OkxAlgoPriceTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAlgoPriceType, string>> Mapping =>
    [
        new(OkxAlgoPriceType.Last, "last"),
        new(OkxAlgoPriceType.Index, "index"),
        new(OkxAlgoPriceType.Mark, "mark"),
    ];
}