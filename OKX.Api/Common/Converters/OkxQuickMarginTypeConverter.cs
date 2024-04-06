using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxQuickMarginTypeConverter(bool quotes) : BaseConverter<OkxQuickMarginType>(quotes)
{
    public OkxQuickMarginTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxQuickMarginType, string>> Mapping =>
    [
        new(OkxQuickMarginType.Manual, "manual"),
        new(OkxQuickMarginType.AutoBorrow, "auto_borrow"),
        new(OkxQuickMarginType.AutoRepay, "auto_repay"),
    ];
}