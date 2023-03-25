namespace OKX.Api.Converters;

internal class AlgoPriceTypeConverter : BaseConverter<OkxAlgoPriceType>
{
    public AlgoPriceTypeConverter() : this(true) { }
    public AlgoPriceTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAlgoPriceType, string>> Mapping => new List<KeyValuePair<OkxAlgoPriceType, string>>
    {
        new KeyValuePair<OkxAlgoPriceType, string>(OkxAlgoPriceType.Last, "last"),
        new KeyValuePair<OkxAlgoPriceType, string>(OkxAlgoPriceType.Index, "index"),
        new KeyValuePair<OkxAlgoPriceType, string>(OkxAlgoPriceType.Mark, "mark"),
    };
}