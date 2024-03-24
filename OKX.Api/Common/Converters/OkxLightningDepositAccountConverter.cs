namespace OKX.Api.Common.Converters;

internal class OkxLightningDepositAccountConverter : BaseConverter<OkxLightningDepositAccount>
{
    public OkxLightningDepositAccountConverter() : this(true) { }
    public OkxLightningDepositAccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxLightningDepositAccount, string>> Mapping =>
    [
        new(OkxLightningDepositAccount.Spot, "1"),
        new(OkxLightningDepositAccount.Funding, "6"),
    ];
}