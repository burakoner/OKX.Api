namespace OKX.Api.Public;

internal class OkxPublicConvertOperationConverter(bool quotes) : BaseConverter<OkxPublicConvertOperation>(quotes)
{
    public OkxPublicConvertOperationConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicConvertOperation, string>> Mapping =>
    [
        new(OkxPublicConvertOperation.Open, "open"),
        new(OkxPublicConvertOperation.Close, "close"),
    ];
}