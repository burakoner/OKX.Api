using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

public class OkxIsolatedMarginModeConverter(bool quotes) : BaseConverter<OkxIsolatedMarginMode>(quotes)
{
    public OkxIsolatedMarginModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxIsolatedMarginMode, string>> Mapping =>
    [
        new(OkxIsolatedMarginMode.AutoTransfer, "automatic"),
        new(OkxIsolatedMarginMode.ManualTransfer, "autonomy"),
        new(OkxIsolatedMarginMode.QuickMarginMode, "quick_margin"),
    ];
}