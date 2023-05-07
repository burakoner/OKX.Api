namespace OKX.Api.Converters;

internal class LoanTypeConverter : BaseConverter<OkxLoanType>
{
    public LoanTypeConverter() : this(true) { }
    public LoanTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxLoanType, string>> Mapping => new()
    {
        new KeyValuePair<OkxLoanType, string>(OkxLoanType.VipLoans, "1"),
        new KeyValuePair<OkxLoanType, string>(OkxLoanType.MarketLoans, "2"),
    };
}   