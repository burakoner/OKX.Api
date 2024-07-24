using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxFundingDepositStateConverter(bool quotes) : BaseConverter<OkxFundingDepositState>(quotes)
{
    public OkxFundingDepositStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingDepositState, string>> Mapping =>
    [
        new(OkxFundingDepositState.WaitingForConfirmation, "1"),
        new(OkxFundingDepositState.Credited, "2"),
        new(OkxFundingDepositState.Successful, "3"),
        new(OkxFundingDepositState.Pending, "4"),
    ];
}