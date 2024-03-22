using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxGreeksTypeConverter : BaseConverter<OkxGreeksType>
{
    public OkxGreeksTypeConverter() : this(true) { }
    public OkxGreeksTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGreeksType, string>> Mapping => new List<KeyValuePair<OkxGreeksType, string>>
    {
        new(OkxGreeksType.GreeksInCoins, "PA"),
        new(OkxGreeksType.BlackScholesGreeksInDollars, "BS"),
        new(OkxGreeksType.EmpiricalGreeks, "CASH"),
    };
}