namespace OKX.Api.Account;

internal class OkxAccountFixedLoanBorrowingTypeConverter(bool quotes) : BaseConverter<OkxAccountFixedLoanBorrowingType>(quotes)
{
    public OkxAccountFixedLoanBorrowingTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountFixedLoanBorrowingType, string>> Mapping =>
    [
        new(OkxAccountFixedLoanBorrowingType.Normal, "normal"),
        new(OkxAccountFixedLoanBorrowingType.Reborrow, "reborrow"),
    ];
}