using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxMarginAddReduceConverter : BaseConverter<OkxMarginAddReduce>
{
    public OkxMarginAddReduceConverter() : this(true) { }
    public OkxMarginAddReduceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMarginAddReduce, string>> Mapping =>
    [
        new(OkxMarginAddReduce.Add, "add"),
        new(OkxMarginAddReduce.Reduce, "reduce"),
    ];
}