using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountVipLoanStateConverter(bool quotes) : BaseConverter<OkxAccountVipLoanState>(quotes)
{
    public OkxAccountVipLoanStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountVipLoanState, string>> Mapping =>
    [
        new(OkxAccountVipLoanState.Borrowing, "1"),
        new(OkxAccountVipLoanState.Borrowed, "2"),
        new(OkxAccountVipLoanState.Repaying, "3"),
        new(OkxAccountVipLoanState.Repaid, "4"),
        new(OkxAccountVipLoanState.BorrowFailed, "5"),
    ];
}