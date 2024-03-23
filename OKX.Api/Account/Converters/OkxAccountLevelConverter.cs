using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountLevelConverter : BaseConverter<OkxAccountLevel>
{
    public OkxAccountLevelConverter() : this(true) { }
    public OkxAccountLevelConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountLevel, string>> Mapping =>
    [
        new(OkxAccountLevel.Simple, "1"),
        new(OkxAccountLevel.SingleCurrencyMargin, "2"),
        new(OkxAccountLevel.MultiCurrencyMargin, "3"),
        new(OkxAccountLevel.PortfolioMargin, "4"),
    ];
}