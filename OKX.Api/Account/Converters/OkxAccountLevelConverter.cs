using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

public class OkxAccountLevelConverter(bool quotes) : BaseConverter<OkxAccountLevel>(quotes)
{
    public OkxAccountLevelConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountLevel, string>> Mapping =>
    [
        new(OkxAccountLevel.Simple, "1"),
        new(OkxAccountLevel.SingleCurrencyMargin, "2"),
        new(OkxAccountLevel.MultiCurrencyMargin, "3"),
        new(OkxAccountLevel.PortfolioMargin, "4"),
    ];
}