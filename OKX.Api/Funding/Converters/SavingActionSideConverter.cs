using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class SavingActionSideConverter : BaseConverter<OkxSavingActionSide>
{
    public SavingActionSideConverter() : this(true) { }
    public SavingActionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSavingActionSide, string>> Mapping =>
    [
        new(OkxSavingActionSide.Purchase, "purchase"),
        new(OkxSavingActionSide.Redempt, "redempt"),
    ];
}