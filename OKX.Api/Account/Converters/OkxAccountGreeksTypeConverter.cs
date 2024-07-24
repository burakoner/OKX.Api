using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountGreeksTypeConverter(bool quotes) : BaseConverter<OkxAccountGreeksType>(quotes)
{
    public OkxAccountGreeksTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountGreeksType, string>> Mapping =>
    [
        new(OkxAccountGreeksType.GreeksInCoins, "PA"),
        new(OkxAccountGreeksType.BlackScholesGreeksInDollars, "BS"),
        new(OkxAccountGreeksType.EmpiricalGreeks, "CASH"),
    ];
}