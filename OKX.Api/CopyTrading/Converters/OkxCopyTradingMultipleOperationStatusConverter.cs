namespace OKX.Api.Account;

internal class OkxCopyTradingMultipleOperationStatusConverter(bool quotes) : BaseConverter<OkxCopyTradingMultipleOperationStatus>(quotes)
{
    public OkxCopyTradingMultipleOperationStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingMultipleOperationStatus, string>> Mapping =>
    [
        new(OkxCopyTradingMultipleOperationStatus.AllSuccess, "0"),
        new(OkxCopyTradingMultipleOperationStatus.SomeSuccesses, "1"),
        new(OkxCopyTradingMultipleOperationStatus.AllFail, "2"),
    ];
}