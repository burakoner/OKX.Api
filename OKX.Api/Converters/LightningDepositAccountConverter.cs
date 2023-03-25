namespace OKX.Api.Converters;

internal class LightningDepositAccountConverter : BaseConverter<OkxLightningDepositAccount>
{
    public LightningDepositAccountConverter() : this(true) { }
    public LightningDepositAccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxLightningDepositAccount, string>> Mapping => new List<KeyValuePair<OkxLightningDepositAccount, string>>
    {
        new KeyValuePair<OkxLightningDepositAccount, string>(OkxLightningDepositAccount.Spot, "1"),
        new KeyValuePair<OkxLightningDepositAccount, string>(OkxLightningDepositAccount.Funding, "6"),
    };
}