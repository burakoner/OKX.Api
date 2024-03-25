using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountConverter(bool quotes) : BaseConverter<OkxAccount>(quotes)
{
    public OkxAccountConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccount, string>> Mapping =>
    [
        new(OkxAccount.Funding, "6"),
        new(OkxAccount.Trading, "18"),
    ];
}