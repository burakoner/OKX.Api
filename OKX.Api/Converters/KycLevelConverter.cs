namespace OKX.Api.Converters;

internal class KycLevelConverter : BaseConverter<OkxKycLevel>
{
    public KycLevelConverter() : this(true) { }
    public KycLevelConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxKycLevel, string>> Mapping => new List<KeyValuePair<OkxKycLevel, string>>
    {
        new KeyValuePair<OkxKycLevel, string>(OkxKycLevel.NoVerification, "0"),
        new KeyValuePair<OkxKycLevel, string>(OkxKycLevel.Level1, "1"),
        new KeyValuePair<OkxKycLevel, string>(OkxKycLevel.Level2, "2"),
        new KeyValuePair<OkxKycLevel, string>(OkxKycLevel.Level3, "3"),
    };
}