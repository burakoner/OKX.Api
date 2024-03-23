using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountLevelConverter : BaseConverter<OkxAccountLevel>
{
    public OkxAccountLevelConverter() : this(true) { }
    public OkxAccountLevelConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountLevel, string>> Mapping => new()
    {
        new KeyValuePair<OkxAccountLevel, string>(OkxAccountLevel.Simple, "1"),
        new KeyValuePair<OkxAccountLevel, string>(OkxAccountLevel.SingleCurrencyMargin, "2"),
        new KeyValuePair<OkxAccountLevel, string>(OkxAccountLevel.MultiCurrencyMargin, "3"),
        new KeyValuePair<OkxAccountLevel, string>(OkxAccountLevel.PortfolioMargin, "4"),
    };
}