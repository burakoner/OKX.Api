namespace OKX.Api.Converters;

internal class RiskOffsetTypeConverter : BaseConverter<OkxRiskOffsetType>
{
    public RiskOffsetTypeConverter() : this(true) { }
    public RiskOffsetTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxRiskOffsetType, string>> Mapping => new List<KeyValuePair<OkxRiskOffsetType, string>>
    {
        new KeyValuePair<OkxRiskOffsetType, string>(OkxRiskOffsetType.SpotDerivativesUSDTToBeOffsetted, "1"),
        new KeyValuePair<OkxRiskOffsetType, string>(OkxRiskOffsetType.SpotDerivativesCoinToBeOffsetted, "2"),
        new KeyValuePair<OkxRiskOffsetType, string>(OkxRiskOffsetType.OnlyDerivativesToBeOffsetted, "3"),
    };
}