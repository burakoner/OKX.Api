using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxFixedLoanBorrowingOrderStateConverter(bool quotes) : BaseConverter<OkxFixedLoanBorrowingOrderState>(quotes)
{
    public OkxFixedLoanBorrowingOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFixedLoanBorrowingOrderState, string>> Mapping =>
    [
        new(OkxFixedLoanBorrowingOrderState.Borrowing, "1"),
        new(OkxFixedLoanBorrowingOrderState.Borrowed, "2"),
        new(OkxFixedLoanBorrowingOrderState.Settled, "3"),
        new(OkxFixedLoanBorrowingOrderState.Failed, "4"),
        new(OkxFixedLoanBorrowingOrderState.Overdue, "5"),
        new(OkxFixedLoanBorrowingOrderState.Settling, "6"),
        new(OkxFixedLoanBorrowingOrderState.Reborrowing, "7"),
    ];
}