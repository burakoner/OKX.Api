using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountVipLoanTypeConverter : BaseConverter<OkxAccountVipLoanType>
{
    public OkxAccountVipLoanTypeConverter() : this(true) { }
    public OkxAccountVipLoanTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountVipLoanType, string>> Mapping => new List<KeyValuePair<OkxAccountVipLoanType, string>>
    {
        new(OkxAccountVipLoanType.Borrow, "1"),
        new(OkxAccountVipLoanType.Repayment, "2"),
        new(OkxAccountVipLoanType.SystemRepayment, "3"),
        new(OkxAccountVipLoanType.InterestRateRefresh, "4"),
    };
}