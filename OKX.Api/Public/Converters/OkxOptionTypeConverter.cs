using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxOptionTypeConverter : BaseConverter<OkxOptionType>
{
    public OkxOptionTypeConverter() : this(true) { }
    public OkxOptionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOptionType, string>> Mapping =>
    [
        new(OkxOptionType.Call, "C"),
        new(OkxOptionType.Put, "P"),
    ];
}