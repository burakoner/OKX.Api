namespace OKX.Api.Converters;

internal class OptionTypeConverter : BaseConverter<OkxOptionType>
{
    public OptionTypeConverter() : this(true) { }
    public OptionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOptionType, string>> Mapping => new List<KeyValuePair<OkxOptionType, string>>
    {
        new KeyValuePair<OkxOptionType, string>(OkxOptionType.Call, "C"),
        new KeyValuePair<OkxOptionType, string>(OkxOptionType.Put, "P"),
    };
}