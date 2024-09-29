namespace OKX.Api.Account;

internal class OkxAccountMarginAddReduceConverter(bool quotes) : BaseConverter<OkxAccountMarginAddReduce>(quotes)
{
    public OkxAccountMarginAddReduceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountMarginAddReduce, string>> Mapping =>
    [
        new(OkxAccountMarginAddReduce.Add, "add"),
        new(OkxAccountMarginAddReduce.Reduce, "reduce"),
    ];
}