using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxMarginAddReduceConverter(bool quotes) : BaseConverter<OkxMarginAddReduce>(quotes)
{
    public OkxMarginAddReduceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxMarginAddReduce, string>> Mapping =>
    [
        new(OkxMarginAddReduce.Add, "add"),
        new(OkxMarginAddReduce.Reduce, "reduce"),
    ];
}