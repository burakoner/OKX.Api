namespace OKX.Api.Converters;

internal class QuickMarginTypeConverter : BaseConverter<OkxQuickMarginType>
{
    public QuickMarginTypeConverter() : this(true) { }
    public QuickMarginTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxQuickMarginType, string>> Mapping => new()
    {
        new KeyValuePair<OkxQuickMarginType, string>(OkxQuickMarginType.Manual, "manual"),
        new KeyValuePair<OkxQuickMarginType, string>(OkxQuickMarginType.AutoBorrow, "auto_borrow"),
        new KeyValuePair<OkxQuickMarginType, string>(OkxQuickMarginType.AutoRepay, "auto_repay"),
    };
}