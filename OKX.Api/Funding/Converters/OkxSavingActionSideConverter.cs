using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxSavingActionSideConverter : BaseConverter<OkxSavingActionSide>
{
    public OkxSavingActionSideConverter() : this(true) { }
    public OkxSavingActionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSavingActionSide, string>> Mapping =>
    [
        new(OkxSavingActionSide.Purchase, "purchase"),
        new(OkxSavingActionSide.Redempt, "redempt"),
    ];
}