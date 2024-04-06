using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxContractTypeConverter(bool quotes) : BaseConverter<OkxContractType>(quotes)
{
    public OkxContractTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxContractType, string>> Mapping =>
    [
        new(OkxContractType.Linear, "linear"),
        new(OkxContractType.Inverse, "inverse"),
    ];
}