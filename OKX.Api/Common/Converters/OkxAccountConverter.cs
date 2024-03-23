namespace OKX.Api.Common.Converters;

internal class OkxAccountConverter : BaseConverter<OkxAccount>
{
    public OkxAccountConverter() : this(true) { }
    public OkxAccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccount, string>> Mapping =>
    [
        new(OkxAccount.Funding, "6"),
        new(OkxAccount.Trading, "18"),
    ];
}