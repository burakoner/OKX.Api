namespace OKX.Api.Converters;

internal class ContractTypeConverter : BaseConverter<OkxContractType>
{
    public ContractTypeConverter() : this(true) { }
    public ContractTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxContractType, string>> Mapping => new List<KeyValuePair<OkxContractType, string>>
    {
        new KeyValuePair<OkxContractType, string>(OkxContractType.Linear, "linear"),
        new KeyValuePair<OkxContractType, string>(OkxContractType.Inverse, "inverse"),
    };
}