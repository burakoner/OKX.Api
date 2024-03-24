using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxSavingActionSideConverter(bool quotes) : BaseConverter<OkxSavingActionSide>(quotes)
{
    public OkxSavingActionSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSavingActionSide, string>> Mapping =>
    [
        new(OkxSavingActionSide.Purchase, "purchase"),
        new(OkxSavingActionSide.Redempt, "redempt"),
    ];
}