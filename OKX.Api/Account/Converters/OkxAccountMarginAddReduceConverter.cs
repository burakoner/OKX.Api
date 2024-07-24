using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountMarginAddReduceConverter(bool quotes) : BaseConverter<OkxAccountMarginAddReduce>(quotes)
{
    public OkxAccountMarginAddReduceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountMarginAddReduce, string>> Mapping =>
    [
        new(OkxAccountMarginAddReduce.Add, "add"),
        new(OkxAccountMarginAddReduce.Reduce, "reduce"),
    ];
}