using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxContractTypeConverter : BaseConverter<OkxContractType>
{
    public OkxContractTypeConverter() : this(true) { }
    public OkxContractTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxContractType, string>> Mapping => new List<KeyValuePair<OkxContractType, string>>
    {
        new(OkxContractType.Linear, "linear"),
        new(OkxContractType.Inverse, "inverse"),
    };
}