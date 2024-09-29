namespace OKX.Api.Common;

internal class OkxKycLevelConverter(bool quotes) : BaseConverter<OkxKycLevel>(quotes)
{
    public OkxKycLevelConverter() : this(true) { }

    protected override List<KeyValuePair<OkxKycLevel, string>> Mapping =>
    [
        new(OkxKycLevel.NoVerification, "0"),
        new(OkxKycLevel.Level1, "1"),
        new(OkxKycLevel.Level2, "2"),
        new(OkxKycLevel.Level3, "3"),
    ];
}