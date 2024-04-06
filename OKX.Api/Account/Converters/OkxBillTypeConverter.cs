using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

public class OkxBillTypeConverter(bool quotes) : BaseConverter<OkxBillType>(quotes)
{
    public OkxBillTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxBillType, string>> Mapping =>
    [
        new(OkxBillType.Transfer, "1"),
        new(OkxBillType.Trade, "2"),
        new(OkxBillType.Delivery, "3"),
        new(OkxBillType.AutoTokenConversion, "4"),
        new(OkxBillType.Liquidation, "5"),
        new(OkxBillType.MarginTransfer, "6"),
        new(OkxBillType.InterestDeduction, "7"),
        new(OkxBillType.FundingFee, "8"),
        new(OkxBillType.ADL, "9"),
        new(OkxBillType.Clawback, "10"),
        new(OkxBillType.SystemTokenConversion, "11"),
        new(OkxBillType.StrategyTransfer, "12"),
        new(OkxBillType.DDH, "13"),
        new(OkxBillType.BlockTrade, "14"),
        new(OkxBillType.QuickMargin, "15"),
        new(OkxBillType.ProfitSharing, "18"),
        new(OkxBillType.Repay, "22"),
        new(OkxBillType.SpreadTrading, "24"),
    ];
}