using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxRiskOffsetTypeConverter(bool quotes) : BaseConverter<OkxRiskOffsetType>(quotes)
{
    public OkxRiskOffsetTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxRiskOffsetType, string>> Mapping =>
    [
        new(OkxRiskOffsetType.UsdtDerivatives, "1"),
        new(OkxRiskOffsetType.CryptoDerivatives, "2"),
        new(OkxRiskOffsetType.DerivativesOnly, "3"),
    ];
}