namespace OKX.Api.Converters;

internal class DepositTypeConverter : BaseConverter<OkxDepositType>
{
    public DepositTypeConverter() : this(true) { }
    public DepositTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxDepositType, string>> Mapping => new List<KeyValuePair<OkxDepositType, string>>
    {
        new KeyValuePair<OkxDepositType, string>(OkxDepositType.InternalDeposit, "3"),
        new KeyValuePair<OkxDepositType, string>(OkxDepositType.BlockchainDeposit, "4"),
    };
}