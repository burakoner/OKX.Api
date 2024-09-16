using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountRiskOffsetTypeConverter(bool quotes) : BaseConverter<OkxAccountRiskOffsetType>(quotes)
{
    public OkxAccountRiskOffsetTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountRiskOffsetType, string>> Mapping =>
    [
        new(OkxAccountRiskOffsetType.UsdtDerivativesUsdt, "1"),
        new(OkxAccountRiskOffsetType.CryptoDerivatives, "2"),
        new(OkxAccountRiskOffsetType.DerivativesOnly, "3"),
        new(OkxAccountRiskOffsetType.UsdtDerivativesUsdc, "4"),
    ];
}