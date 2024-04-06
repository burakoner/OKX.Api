using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxRiskOffsetTypeConverter(bool quotes) : BaseConverter<OkxRiskOffsetType>(quotes)
{
    public OkxRiskOffsetTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxRiskOffsetType, string>> Mapping =>
    [
        new(OkxRiskOffsetType.SpotDerivativesUSDTToBeOffsetted, "1"),
        new(OkxRiskOffsetType.SpotDerivativesCoinToBeOffsetted, "2"),
        new(OkxRiskOffsetType.OnlyDerivativesToBeOffsetted, "3"),
    ];
}