namespace OKX.Api.Converters;

internal class AccountBillTypeConverter : BaseConverter<OkxAccountBillType>
{
    public AccountBillTypeConverter() : this(true) { }
    public AccountBillTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountBillType, string>> Mapping => new()
    {
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.Transfer, "1"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.Trade, "2"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.Delivery, "3"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.AutoTokenConversion, "4"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.Liquidation, "5"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.MarginTransfer, "6"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.InterestDeduction, "7"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.FundingFee, "8"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.ADL, "9"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.Clawback, "10"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.SystemTokenConversion, "11"),
        new KeyValuePair<OkxAccountBillType, string>(OkxAccountBillType.StrategyTransfer, "12"),
    };
}