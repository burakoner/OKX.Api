using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxLightningDepositAccountConverter(bool quotes) : BaseConverter<OkxLightningDepositAccount>(quotes)
{
    public OkxLightningDepositAccountConverter() : this(true) { }

    protected override List<KeyValuePair<OkxLightningDepositAccount, string>> Mapping =>
    [
        new(OkxLightningDepositAccount.Spot, "1"),
        new(OkxLightningDepositAccount.Funding, "6"),
    ];
}