using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxDerivativesOffsetModeConverter : BaseConverter<OkxDerivativesOffsetMode>
{
    public OkxDerivativesOffsetModeConverter() : this(true) { }
    public OkxDerivativesOffsetModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxDerivativesOffsetMode, string>> Mapping => new List<KeyValuePair<OkxDerivativesOffsetMode, string>>
    {
        new(OkxDerivativesOffsetMode.UsdtDerivatives, "1"),
        new(OkxDerivativesOffsetMode.CryptoDerivatives, "2"),
        new(OkxDerivativesOffsetMode.DerivativesOnly, "3"),
    };
}