namespace OKX.Api.Account;

internal class OkxAccountDiscountTypeConverter(bool quotes) : BaseConverter<OkxAccountDiscountType>(quotes)
{
    public OkxAccountDiscountTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountDiscountType, string>> Mapping =>
    [
        new(OkxAccountDiscountType.OriginalDiscountRateRules, "0"),
        new(OkxAccountDiscountType.NewDiscountRules, "1"),
    ];
}