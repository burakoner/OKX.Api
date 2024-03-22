using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxIsolatedMarginModeConverter : BaseConverter<OkxIsolatedMarginMode>
{
    public OkxIsolatedMarginModeConverter() : this(true) { }
    public OkxIsolatedMarginModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxIsolatedMarginMode, string>> Mapping => new List<KeyValuePair<OkxIsolatedMarginMode, string>>
    {
        new(OkxIsolatedMarginMode.AutoTransfer, "automatic"),
        new(OkxIsolatedMarginMode.ManualTransfer, "autonomy"),
        new(OkxIsolatedMarginMode.QuickMarginMode, "quick_margin"),
    };
}