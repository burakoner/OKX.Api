using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountIsolatedMarginModeConverter(bool quotes) : BaseConverter<OkxAccountIsolatedMarginMode>(quotes)
{
    public OkxAccountIsolatedMarginModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountIsolatedMarginMode, string>> Mapping =>
    [
        new(OkxAccountIsolatedMarginMode.AutoTransfer, "automatic"),
        new(OkxAccountIsolatedMarginMode.ManualTransfer, "autonomy"),
        new(OkxAccountIsolatedMarginMode.QuickMarginMode, "quick_margin"),
    ];
}