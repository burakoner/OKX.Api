using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountVipLoanStateConverter : BaseConverter<OkxAccountVipLoanState>
{
    public OkxAccountVipLoanStateConverter() : this(true) { }
    public OkxAccountVipLoanStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountVipLoanState, string>> Mapping =>
    [
        new(OkxAccountVipLoanState.Borrowing, "1"),
        new(OkxAccountVipLoanState.Borrowed, "2"),
        new(OkxAccountVipLoanState.Repaying, "3"),
        new(OkxAccountVipLoanState.Repaid, "4"),
        new(OkxAccountVipLoanState.BorrowFailed, "5"),
    ];
}