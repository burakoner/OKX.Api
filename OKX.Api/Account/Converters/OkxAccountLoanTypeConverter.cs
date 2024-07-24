using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountLoanTypeConverter(bool quotes) : BaseConverter<OkxAccountLoanType>(quotes)
{
    public OkxAccountLoanTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountLoanType, string>> Mapping =>
    [
        new(OkxAccountLoanType.VIP, "1"),
        new(OkxAccountLoanType.Market, "2"),
    ];
}