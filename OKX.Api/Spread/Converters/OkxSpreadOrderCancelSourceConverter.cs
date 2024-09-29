namespace OKX.Api.Spread;

internal class OkxSpreadOrderCancelSourceConverter(bool quotes) : BaseConverter<OkxSpreadOrderCancelSource>(quotes)
{
    public OkxSpreadOrderCancelSourceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSpreadOrderCancelSource, string>> Mapping =>
    [
        new(OkxSpreadOrderCancelSource.System, "0"),
        new(OkxSpreadOrderCancelSource.User, "1"),
        new(OkxSpreadOrderCancelSource.PartiallyCanceled, "14"),
        new(OkxSpreadOrderCancelSource.PostOnlyOrderTakeLiquidity, "31"),
        new(OkxSpreadOrderCancelSource.InsufficientMargin, "34"),
        new(OkxSpreadOrderCancelSource.InsufficientMarginFromAnotherOrder, "35"),
    ];
}