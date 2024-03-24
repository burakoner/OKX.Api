namespace OKX.Api.Common.Converters;

internal class OkxKycLevelConverter : BaseConverter<OkxKycLevel>
{
    public OkxKycLevelConverter() : this(true) { }
    public OkxKycLevelConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxKycLevel, string>> Mapping =>
    [
        new(OkxKycLevel.NoVerification, "0"),
        new(OkxKycLevel.Level1, "1"),
        new(OkxKycLevel.Level2, "2"),
        new(OkxKycLevel.Level3, "3"),
    ];
}