using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxLoanTypeConverter : BaseConverter<OkxLoanType>
{
    public OkxLoanTypeConverter() : this(true) { }
    public OkxLoanTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxLoanType, string>> Mapping => new List<KeyValuePair<OkxLoanType, string>>
    {
        new(OkxLoanType.VIP, "1"),
        new(OkxLoanType.Market, "2"),
    };
}