namespace OKX.Api.Common.Converters;

internal class OkxQuickMarginTypeConverter : BaseConverter<OkxQuickMarginType>
{
    public OkxQuickMarginTypeConverter() : this(true) { }
    public OkxQuickMarginTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxQuickMarginType, string>> Mapping =>
    [
        new(OkxQuickMarginType.Manual, "manual"),
        new(OkxQuickMarginType.AutoBorrow, "auto_borrow"),
        new(OkxQuickMarginType.AutoRepay, "auto_repay"),
    ];
}