namespace OKX.Api.Common.Converters;

internal class OkxOrderCategoryConverter : BaseConverter<OkxOrderCategory>
{
    public OkxOrderCategoryConverter() : this(true) { }
    public OkxOrderCategoryConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderCategory, string>> Mapping =>
    [
        new(OkxOrderCategory.Normal, "normal"),
        new(OkxOrderCategory.TWAP, "twap"),
        new(OkxOrderCategory.ADL, "adl"),
        new(OkxOrderCategory.FullLiquidation, "full_liquidation"),
        new(OkxOrderCategory.PartialLiquidation, "partial_liquidation"),
        new(OkxOrderCategory.Delivery, "delivery"),
        new(OkxOrderCategory.DDH, "ddh"),
    ];
}