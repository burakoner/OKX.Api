namespace OKX.Api.Converters;

internal class IsolatedMarginModeConverter : BaseConverter<OkxIsolatedMarginMode>
{
    public IsolatedMarginModeConverter() : this(true) { }
    public IsolatedMarginModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxIsolatedMarginMode, string>> Mapping => new List<KeyValuePair<OkxIsolatedMarginMode, string>>
    {
        new KeyValuePair<OkxIsolatedMarginMode, string>(OkxIsolatedMarginMode.AutoTransfer, "automatic"),
        new KeyValuePair<OkxIsolatedMarginMode, string>(OkxIsolatedMarginMode.ManualTransfer, "autonomy"),
        new KeyValuePair<OkxIsolatedMarginMode, string>(OkxIsolatedMarginMode.QuickMarginMode, "quick_margin"),
    };
}