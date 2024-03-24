namespace OKX.Api.Common.Converters;

internal class OkxRiskOffsetTypeConverter : BaseConverter<OkxRiskOffsetType>
{
    public OkxRiskOffsetTypeConverter() : this(true) { }
    public OkxRiskOffsetTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxRiskOffsetType, string>> Mapping =>
    [
        new(OkxRiskOffsetType.SpotDerivativesUSDTToBeOffsetted, "1"),
        new(OkxRiskOffsetType.SpotDerivativesCoinToBeOffsetted, "2"),
        new(OkxRiskOffsetType.OnlyDerivativesToBeOffsetted, "3"),
    ];
}