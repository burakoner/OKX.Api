namespace OKX.Api.Converters;

internal class DepositStateConverter : BaseConverter<OkxDepositState>
{
    public DepositStateConverter() : this(true) { }
    public DepositStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxDepositState, string>> Mapping => new List<KeyValuePair<OkxDepositState, string>>
    {
        new KeyValuePair<OkxDepositState, string>(OkxDepositState.WaitingForConfirmation, "1"),
        new KeyValuePair<OkxDepositState, string>(OkxDepositState.Credited, "2"),
        new KeyValuePair<OkxDepositState, string>(OkxDepositState.Successful, "3"),
        new KeyValuePair<OkxDepositState, string>(OkxDepositState.Pending, "4"),
    };
}