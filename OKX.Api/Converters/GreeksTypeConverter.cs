namespace OKX.Api.Converters;

internal class GreeksTypeConverter : BaseConverter<OkxGreeksType>
{
    public GreeksTypeConverter() : this(true) { }
    public GreeksTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGreeksType, string>> Mapping => new List<KeyValuePair<OkxGreeksType, string>>
    {
        new KeyValuePair<OkxGreeksType, string>(OkxGreeksType.GreeksInCoins, "PA"),
        new KeyValuePair<OkxGreeksType, string>(OkxGreeksType.BlackScholesGreeksInDollars, "BS"),
    };
}