using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

public class OkxDerivativesOffsetModeConverter(bool quotes) : BaseConverter<OkxDerivativesOffsetMode>(quotes)
{
    public OkxDerivativesOffsetModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxDerivativesOffsetMode, string>> Mapping =>
    [
        new(OkxDerivativesOffsetMode.UsdtDerivatives, "1"),
        new(OkxDerivativesOffsetMode.CryptoDerivatives, "2"),
        new(OkxDerivativesOffsetMode.DerivativesOnly, "3"),
    ];
}