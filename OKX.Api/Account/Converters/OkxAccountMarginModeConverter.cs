using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountMarginModeConverter(bool quotes) : BaseConverter<OkxAccountMarginMode>(quotes)
{
    public OkxAccountMarginModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountMarginMode, string>> Mapping =>
    [
        new(OkxAccountMarginMode.Isolated, "isolated"),
        new(OkxAccountMarginMode.Cross, "cross"),
    ];
}