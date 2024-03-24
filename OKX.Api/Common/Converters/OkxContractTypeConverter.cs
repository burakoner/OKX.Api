namespace OKX.Api.Common.Converters;

internal class OkxContractTypeConverter(bool quotes) : BaseConverter<OkxContractType>(quotes)
{
    public OkxContractTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxContractType, string>> Mapping =>
    [
        new(OkxContractType.Linear, "linear"),
        new(OkxContractType.Inverse, "inverse"),
    ];
}