using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

public class OkxGreeksTypeConverter(bool quotes) : BaseConverter<OkxGreeksType>(quotes)
{
    public OkxGreeksTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGreeksType, string>> Mapping =>
    [
        new(OkxGreeksType.GreeksInCoins, "PA"),
        new(OkxGreeksType.BlackScholesGreeksInDollars, "BS"),
        new(OkxGreeksType.EmpiricalGreeks, "CASH"),
    ];
}