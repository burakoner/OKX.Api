namespace OKX.Api.Algo;

internal class OkxAlgoOrderKindConverter(bool quotes) : BaseConverter<OkxAlgoOrderKind>(quotes)
{
    public OkxAlgoOrderKindConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAlgoOrderKind, string>> Mapping =>
    [
        new(OkxAlgoOrderKind.Condition, "condition"),
        new(OkxAlgoOrderKind.Limit, "limit"),
    ];
}