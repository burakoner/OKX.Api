using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountBillTypeConverter : BaseConverter<OkxAccountBillType>
{
    public OkxAccountBillTypeConverter() : this(true) { }
    public OkxAccountBillTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountBillType, string>> Mapping =>
    [
        new(OkxAccountBillType.Transfer, "1"),
        new(OkxAccountBillType.Trade, "2"),
        new(OkxAccountBillType.Delivery, "3"),
        new(OkxAccountBillType.AutoTokenConversion, "4"),
        new(OkxAccountBillType.Liquidation, "5"),
        new(OkxAccountBillType.MarginTransfer, "6"),
        new(OkxAccountBillType.InterestDeduction, "7"),
        new(OkxAccountBillType.FundingFee, "8"),
        new(OkxAccountBillType.ADL, "9"),
        new(OkxAccountBillType.Clawback, "10"),
        new(OkxAccountBillType.SystemTokenConversion, "11"),
        new(OkxAccountBillType.StrategyTransfer, "12"),
        new(OkxAccountBillType.DDH, "13"),
        new(OkxAccountBillType.BlockTrade, "14"),
        new(OkxAccountBillType.QuickMargin, "15"),
        new(OkxAccountBillType.ProfitSharing, "18"),
        new(OkxAccountBillType.Repay, "22"),
        new(OkxAccountBillType.SpreadTrading, "24"),
    ];
}