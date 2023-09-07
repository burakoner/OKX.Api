namespace OKX.Api.Converters;

internal class MarginTransferModeConverter : BaseConverter<OkxMarginTransferMode>
{
    public MarginTransferModeConverter() : this(true) { }
    public MarginTransferModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMarginTransferMode, string>> Mapping => new List<KeyValuePair<OkxMarginTransferMode, string>>
    {
        new KeyValuePair<OkxMarginTransferMode, string>(OkxMarginTransferMode.AutoTransfer, "automatic"),
        new KeyValuePair<OkxMarginTransferMode, string>(OkxMarginTransferMode.ManualTransfer, "autonomy"),
        new KeyValuePair<OkxMarginTransferMode, string>(OkxMarginTransferMode.QuickMarginMode, "quick_margin"),
    };
}