namespace OKX.Api.Financial.FlexibleSimpleEarn;

internal class OkxFlexibleSimpleEarnSavingsSideConverter(bool quotes) : BaseConverter<OkxFlexibleSimpleEarnSavingsSide>(quotes)
{
    public OkxFlexibleSimpleEarnSavingsSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFlexibleSimpleEarnSavingsSide, string>> Mapping =>
    [
        new(OkxFlexibleSimpleEarnSavingsSide.Purchase, "purchase"),
        new(OkxFlexibleSimpleEarnSavingsSide.Redeem, "redempt"),
    ];
}