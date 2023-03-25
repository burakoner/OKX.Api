namespace OKX.Api.Converters;

internal class FeeRateCategoryConverter : BaseConverter<OkxFeeRateCategory>
{
    public FeeRateCategoryConverter() : this(true) { }
    public FeeRateCategoryConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxFeeRateCategory, string>> Mapping => new List<KeyValuePair<OkxFeeRateCategory, string>>
    {
        new KeyValuePair<OkxFeeRateCategory, string>(OkxFeeRateCategory.ClassA, "1"),
        new KeyValuePair<OkxFeeRateCategory, string>(OkxFeeRateCategory.ClassB, "2"),
        new KeyValuePair<OkxFeeRateCategory, string>(OkxFeeRateCategory.ClassC, "3"),
        new KeyValuePair<OkxFeeRateCategory, string>(OkxFeeRateCategory.ClassD, "4"),
    };
}