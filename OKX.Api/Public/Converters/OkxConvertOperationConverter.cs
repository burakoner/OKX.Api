namespace OKX.Api.Public;

internal class OkxConvertOperationConverter(bool quotes) : BaseConverter<OkxConvertOperation>(quotes)
{
    public OkxConvertOperationConverter() : this(true) { }

    protected override List<KeyValuePair<OkxConvertOperation, string>> Mapping =>
    [
        new(OkxConvertOperation.Open, "open"),
        new(OkxConvertOperation.Close, "close"),
    ];
}