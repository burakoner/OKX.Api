namespace OKX.Api.Converters;

internal class AccountBillSubTypeConverter : BaseConverter<OkxAccountBillSubType>
{
    public AccountBillSubTypeConverter() : this(true) { }
    public AccountBillSubTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountBillSubType, string>> Mapping => new()
    {
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.Buy, "1"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.Sell, "2"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.OpenLong, "3"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.OpenShort, "4"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.CloseLong, "5"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.CloseShort, "6"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.InterestDeduction, "9"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.TransferIn, "11"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.TransferOut, "12"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ManualMarginIncrease, "160"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ManualMarginDecrease, "161"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.AutoMarginIncrease, "162"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.AutoBuy, "110"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.AutoSell, "111"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.SystemTokenConversionTransferIn, "118"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.SystemTokenConversionTransferOut, "119"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.PartialLiquidationCloseLong, "100"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.PartialLiquidationCloseShort, "101"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.PartialLiquidationBuy, "102"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.PartialLiquidationSell, "103"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.LiquidationLong, "104"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.LiquidationShort, "105"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.LiquidationBuy, "106"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.LiquidationSell, "107"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.LiquidationTransferIn, "110"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.LiquidationTransferOut, "111"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ADLCloseLong, "125"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ADLCloseShort, "126"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ADLBuy, "127"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ADLSell, "128"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.Exercised, "170"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.CounterpartyExercised, "171"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ExpiredOTM, "172"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.DeliveryLong, "112"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.DeliveryShort, "113"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.DeliveryExerciseClawback, "117"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.FundingFeeExpense, "173"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.FundingFeeIncome, "174"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.SystemTransferIn, "200"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ManuallyTransferIn, "201"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.SystemTransferOut, "202"),
            new KeyValuePair<OkxAccountBillSubType, string>(OkxAccountBillSubType.ManuallyTransferOut, "203"),
        };
}