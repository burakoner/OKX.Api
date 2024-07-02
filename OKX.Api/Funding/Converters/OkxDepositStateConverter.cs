using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxDepositStateConverter(bool quotes) : BaseConverter<OkxDepositState>(quotes)
{
    public OkxDepositStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxDepositState, string>> Mapping =>
    [
        new(OkxDepositState.WaitingForConfirmation, "1"),
        new(OkxDepositState.Credited, "2"),
        new(OkxDepositState.Successful, "3"),
        new(OkxDepositState.Pending, "4"),
    ];
}