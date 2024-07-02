namespace OKX.Api.Common.Converters;

internal class OkxBooleanConverter(bool quotes) : BaseConverter<bool>(quotes)
{
    public OkxBooleanConverter() : this(true) { }

    protected override List<KeyValuePair<bool, string>> Mapping =>
    [
        new(true, "1"),
        new(true, "True"),
        new(true, "true"),
        new(true, "Yes"),
        new(true, "yes"),
        new(true, "T"),
        new(true, "t"),
        new(true, "Y"),
        new(true, "y"),
        new(false, "0"),
        new(false, "False"),
        new(false, "false"),
        new(false, "No"),
        new(false, "no"),
        new(false, "F"),
        new(false, "f"),
        new(false, "N"),
        new(false, "n"),
    ];
}