namespace OKX.Api.Converters;

internal class SavingActionSideConverter : BaseConverter<OkxSavingActionSide>
{
    public SavingActionSideConverter() : this(true) { }
    public SavingActionSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSavingActionSide, string>> Mapping => new List<KeyValuePair<OkxSavingActionSide, string>>
    {
        new KeyValuePair<OkxSavingActionSide, string>(OkxSavingActionSide.Purchase, "purchase"),
        new KeyValuePair<OkxSavingActionSide, string>(OkxSavingActionSide.Redempt, "redempt"),
    };
}