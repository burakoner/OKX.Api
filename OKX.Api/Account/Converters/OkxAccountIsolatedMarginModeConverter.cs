namespace OKX.Api.Account;

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