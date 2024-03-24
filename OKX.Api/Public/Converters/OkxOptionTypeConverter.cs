using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxOptionTypeConverter(bool quotes) : BaseConverter<OkxOptionType>(quotes)
{
    public OkxOptionTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxOptionType, string>> Mapping =>
    [
        new(OkxOptionType.Call, "C"),
        new(OkxOptionType.Put, "P"),
    ];
}