using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxFixedLoanBorrowingTypeConverter(bool quotes) : BaseConverter<OkxFixedLoanBorrowingType>(quotes)
{
    public OkxFixedLoanBorrowingTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFixedLoanBorrowingType, string>> Mapping =>
    [
        new(OkxFixedLoanBorrowingType.Borrow, "normal"),
        new(OkxFixedLoanBorrowingType.Reborrow, "reborrow"),
    ];
}