using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxMarginModeConverter(bool quotes) : BaseConverter<OkxMarginMode>(quotes)
{
    public OkxMarginModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxMarginMode, string>> Mapping =>
    [
        new(OkxMarginMode.Isolated, "isolated"),
        new(OkxMarginMode.Cross, "cross"),
    ];
}