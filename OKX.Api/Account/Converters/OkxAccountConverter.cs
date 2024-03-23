using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountConverter : BaseConverter<OkxAccount>
{
    public OkxAccountConverter() : this(true) { }
    public OkxAccountConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccount, string>> Mapping => new()
    {
        //new KeyValuePair<OkxAccount, string>(OkxAccount.Spot, "1"),
        //new KeyValuePair<OkxAccount, string>(OkxAccount.Futures, "3"),
        //new KeyValuePair<OkxAccount, string>(OkxAccount.Margin, "5"),
        new KeyValuePair<OkxAccount, string>(OkxAccount.Funding, "6"),
        //new KeyValuePair<OkxAccount, string>(OkxAccount.Swap, "9"),
        //new KeyValuePair<OkxAccount, string>(OkxAccount.Option, "12"),
        new KeyValuePair<OkxAccount, string>(OkxAccount.Trading, "18"),
    };
}