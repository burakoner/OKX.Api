namespace OKX.Api.Account;

internal class OkxAccountFixedLoanBorrowingOrderStateConverter(bool quotes) : BaseConverter<OkxAccountFixedLoanBorrowingOrderState>(quotes)
{
    public OkxAccountFixedLoanBorrowingOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountFixedLoanBorrowingOrderState, string>> Mapping =>
    [
        new(OkxAccountFixedLoanBorrowingOrderState.Borrowing, "1"),
        new(OkxAccountFixedLoanBorrowingOrderState.Borrowed, "2"),
        new(OkxAccountFixedLoanBorrowingOrderState.Settled, "3"),
        new(OkxAccountFixedLoanBorrowingOrderState.Failed, "4"),
        new(OkxAccountFixedLoanBorrowingOrderState.Overdue, "5"),
        new(OkxAccountFixedLoanBorrowingOrderState.Settling, "6"),
        new(OkxAccountFixedLoanBorrowingOrderState.Reborrowing, "7"),
    ];
}