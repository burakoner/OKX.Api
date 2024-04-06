using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxKycLevelConverter(bool quotes) : BaseConverter<OkxKycLevel>(quotes)
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