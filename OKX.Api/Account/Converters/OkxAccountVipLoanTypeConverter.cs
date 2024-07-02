using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountVipLoanTypeConverter(bool quotes) : BaseConverter<OkxAccountVipLoanType>(quotes)
{
    public OkxAccountVipLoanTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountVipLoanType, string>> Mapping =>
    [
        new(OkxAccountVipLoanType.Borrow, "1"),
        new(OkxAccountVipLoanType.Repayment, "2"),
        new(OkxAccountVipLoanType.SystemRepayment, "3"),
        new(OkxAccountVipLoanType.InterestRateRefresh, "4"),
    ];
}