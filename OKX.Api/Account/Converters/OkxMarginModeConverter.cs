using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxMarginModeConverter : BaseConverter<OkxMarginMode>
{
    public OkxMarginModeConverter() : this(true) { }
    public OkxMarginModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMarginMode, string>> Mapping =>
    [
        new(OkxMarginMode.Isolated, "isolated"),
        new(OkxMarginMode.Cross, "cross"),
    ];
}