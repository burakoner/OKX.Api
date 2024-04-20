using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.Account.Converters;

public class OkxMultipleOperationStatusConverter(bool quotes) : BaseConverter<OkxMultipleOperationStatus>(quotes)
{
    public OkxMultipleOperationStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxMultipleOperationStatus, string>> Mapping =>
    [
        new(OkxMultipleOperationStatus.AllSuccess, "0"),
        new(OkxMultipleOperationStatus.SomeSuccesses, "1"),
        new(OkxMultipleOperationStatus.AllFail, "2"),
    ];
}