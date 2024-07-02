using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxLoanTypeConverter(bool quotes) : BaseConverter<OkxLoanType>(quotes)
{
    public OkxLoanTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxLoanType, string>> Mapping =>
    [
        new(OkxLoanType.VIP, "1"),
        new(OkxLoanType.Market, "2"),
    ];
}