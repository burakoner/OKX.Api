namespace OKX.Api.Examples;

internal class Program
{
    static async Task Main(string[] args)
    {
        #region Rest Api Client Examples
        var api = new OkxRestApiClient();
        api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

        // Order Book Trading -> Market Data Methods (Signed)
        var market_01 = await api.Public.GetTickersAsync(OkxInstrumentType.Spot);
        var market_02 = await api.Public.GetTickerAsync("BTC-USDT");
        var market_03 = await api.Public.GetOrderBookAsync("BTC-USDT", 40);
        var market_04 = await api.Public.GetOrderBookFullAsync("BTC-USDT", 5000);
        var market_05 = await api.Public.GetCandlesticksAsync("BTC-USDT", "6Hutc");
        var market_06 = await api.Public.GetCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var market_07 = await api.Public.GetCandlestickHistoryAsync("BTC-USDT", "6Hutc");
        var market_08 = await api.Public.GetCandlestickHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        var market_09 = await api.Public.GetTradesAsync("BTC-USDT");
        var market_10 = await api.Public.GetTradeHistoryAsync("BTC-USDT");
        var market_11 = await api.Public.GetOptionTradesByInstrumentFamilyAsync("BTC-USDT");
        var market_12 = await api.Public.GetOptionTradesAsync("BTC-USDT");
        var market_13 = await api.Public.Get24HourVolumeAsync();

        // Public Methods (Unsigned)
        var public_01 = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Spot);
        var public_02 = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Margin);
        var public_03 = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Swap, instrumentId: "BTC-USDT-SWAP");
        var public_04 = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Futures);
        var public_05 = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Option, "USD");
        var public_06 = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Swap, instrumentId: "BTC-USDT-SWAP", signed: true);
        var public_07 = await api.Public.GetDeliveryExerciseHistoryAsync(OkxInstrumentType.Futures, "BTC-USD");
        var public_08 = await api.Public.GetDeliveryExerciseHistoryAsync(OkxInstrumentType.Option, "BTC-USD");
        var public_09 = await api.Public.GetOpenInterestsAsync(OkxInstrumentType.Futures);
        var public_10 = await api.Public.GetOpenInterestsAsync(OkxInstrumentType.Option, "BTC-USD");
        var public_11 = await api.Public.GetOpenInterestsAsync(OkxInstrumentType.Swap, "BTC-USD");
        var public_12 = await api.Public.GetFundingRatesAsync("BTC-USD-SWAP");
        var public_13 = await api.Public.GetFundingRateHistoryAsync("BTC-USD-SWAP");
        var public_14 = await api.Public.GetLimitPriceAsync("BTC-USD-SWAP");
        var public_15 = await api.Public.GetOptionMarketDataAsync("BTC-USD");
        var public_16 = await api.Public.GetEstimatedPriceAsync("BTC-USD-211004-41000-C");
        var public_17 = await api.Public.GetDiscountInfoAsync();
        var public_18 = await api.Public.GetServerTimeAsync();
        var public_19 = await api.Public.GetMarkPricesAsync(OkxInstrumentType.Futures);
        var public_20 = await api.Public.GetPositionTiersAsync(OkxInstrumentType.Futures, OkxAccountMarginMode.Isolated, "BTC-USD");
        var public_21 = await api.Public.GetInterestRatesAsync();
        var public_23 = await api.Public.GetUnderlyingAsync(OkxInstrumentType.Futures);
        var public_24 = await api.Public.GetUnderlyingAsync(OkxInstrumentType.Option);
        var public_25 = await api.Public.GetUnderlyingAsync(OkxInstrumentType.Swap);
        var public_26 = await api.Public.GetInsuranceFundAsync(OkxInstrumentType.Margin, currency: "BTC");
        var public_27 = await api.Public.GetUnitConvertAsync("BTC-USD-SWAP", price: 35000, size: 0.888m);
        var public_28 = await api.Public.GetOptionTickBandsAsync();
        var public_29 = await api.Public.GetPremiumHistoryAsync(instrumentId: "BTC-USDT");
        var public_30 = await api.Public.GetIndexTickersAsync(instrumentId: "BTC-USDT");
        var public_31 = await api.Public.GetIndexCandlesticksAsync("BTC-USDT", "6Hutc");
        var public_32 = await api.Public.GetIndexCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_33 = await api.Public.GetIndexCandlesticksHistoryAsync("BTC-USDT", "6Hutc");
        var public_34 = await api.Public.GetIndexCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_35 = await api.Public.GetMarkPriceCandlesticksAsync("BTC-USDT", "6Hutc");
        var public_36 = await api.Public.GetMarkPriceCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_37 = await api.Public.GetMarkPriceCandlesticksHistoryAsync("BTC-USDT", "6Hutc");
        var public_38 = await api.Public.GetMarkPriceCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_40 = await api.Public.GetExchangeRateAsync();
        var public_41 = await api.Public.GetIndexComponentsAsync("BTC-USDT");
        var public_42 = await api.Public.GetEconomicCalendarDataAsync("BTC-USDT");
        var public_43 = await api.Public.GetAnnouncementTypesAsync();
        var public_44 = await api.Public.GetAnnouncementsAsync();
        var public_45 = await api.Public.GetSystemUpgradeStatusAsync();

        // Trading Account Methods (Signed)
        var account_01 = await api.Account.GetInstrumentsAsync(OkxInstrumentType.Spot);
        var account_02 = await api.Account.GetBalancesAsync();
        var account_03 = await api.Account.GetPositionsAsync();
        var account_04 = await api.Account.GetPositionsHistoryAsync();
        var account_05 = await api.Account.GetPositionRiskAsync();
        var account_06 = await api.Account.GetBillHistoryAsync();
        var account_07 = await api.Account.GetBillArchiveAsync();
        var account_08 = await api.Account.ApplyBillDataAsync(2022, OkxQuarter.Quarter1);
        var account_09 = await api.Account.GetBillDataAsync(2022, OkxQuarter.Quarter1);
        var account_10 = await api.Account.GetConfigurationAsync();
        var account_11 = await api.Account.SetPositionModeAsync(OkxTradePositionMode.LongShortMode);
        var account_12 = await api.Account.GetLeverageAsync("BTC-USD-211008", OkxAccountMarginMode.Isolated);
        var account_13 = await api.Account.SetLeverageAsync(30, null, "BTC-USD-211008", OkxAccountMarginMode.Isolated, OkxTradePositionSide.Long);
        var account_14 = await api.Account.GetMaximumOrderQuantityAsync("BTC-USDT", OkxTradeMode.Isolated);
        var account_15 = await api.Account.GetMaximumAvailableAmountAsync("BTC-USDT", OkxTradeMode.Isolated);
        var account_16 = await api.Account.SetMarginAmountAsync("BTC-USDT", OkxTradePositionSide.Long, OkxAccountMarginAddReduce.Add, 100.0m);
        var account_17 = await api.Account.GetLeverageEstimatedInformationAsync(OkxInstrumentType.Futures, OkxAccountMarginMode.Cross, 10);
        var account_18 = await api.Account.GetMaximumLoanAmountAsync(OkxAccountMarginMode.Cross, "BTC-USDT");
        var account_19 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Spot, OkxInstrumentRuleType.Normal);
        var account_20 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Futures, OkxInstrumentRuleType.Normal);
        var account_21 = await api.Account.GetInterestAccruedAsync();
        var account_22 = await api.Account.GetInterestRateAsync();
        var account_23 = await api.Account.SetGreeksAsync(OkxAccountGreeksType.GreeksInCoins);
        var account_24 = await api.Account.SetIsolatedMarginModeAsync(OkxInstrumentType.Futures, OkxAccountIsolatedMarginMode.AutoTransfer);
        var account_25 = await api.Account.GetMaximumWithdrawalsAsync();
        var account_26 = await api.Account.GetRiskStateAsync();
        var account_40 = await api.Account.GetInterestLimitsAsync();
        var account_48 = await api.Account.PositionBuilderAsync();
        var account_49 = await api.Account.SetRiskOffsetAmountAsync("BTC", 1.0M);
        var account_50 = await api.Account.GetGreeksAsync();
        var account_51 = await api.Account.GetPositionTiersAsync(OkxInstrumentType.Futures);
        var account_53 = await api.Account.ActivateOptionAsync();
        var account_54 = await api.Account.SetAutoLoanAsync(true);
        var account_55 = await api.Account.SetLevelAsync(OkxAccountMode.SpotAndFuturesMode);
        var account_56 = await api.Account.ResetMmpAsync("BTC-USDT");
        var account_57 = await api.Account.SetMmpAsync("BTC-USDT", 5000, 5000, 1);
        var account_58 = await api.Account.GetMmpAsync("BTC-USDT");

        // Order Book Trading -> Trade Methods (Signed)
        var trade_01 = await api.Trade.PlaceOrderAsync("BTC-USDT", OkxTradeMode.Cash, OkxTradeOrderSide.Buy, OkxTradePositionSide.Long, OkxTradeOrderType.MarketOrder, 0.1m);
        var trade_02 = await api.Trade.PlaceOrdersAsync([]);
        var trade_03 = await api.Trade.CancelOrderAsync("BTC-USDT");
        var trade_04 = await api.Trade.CancelOrdersAsync([]);
        var trade_05 = await api.Trade.AmendOrderAsync("BTC-USDT");
        var trade_06 = await api.Trade.AmendOrdersAsync([]);
        var trade_07 = await api.Trade.ClosePositionAsync("BTC-USDT", OkxAccountMarginMode.Isolated);
        var trade_08 = await api.Trade.GetOrderAsync("BTC-USDT");
        var trade_09 = await api.Trade.GetOpenOrdersAsync();
        var trade_10 = await api.Trade.GetOrderHistoryAsync(OkxInstrumentType.Swap);
        var trade_11 = await api.Trade.GetOrderArchiveAsync(OkxInstrumentType.Futures);
        var trade_12 = await api.Trade.GetTradesAsync();
        var trade_13 = await api.Trade.GetTradesHistoryAsync(OkxInstrumentType.Futures);
        var trade_16 = await api.Trade.GetEasyConvertCurrenciesAsync();
        var trade_17 = await api.Trade.PlaceEasyConvertOrderAsync([], "USDT");
        var trade_18 = await api.Trade.GetEasyConvertHistoryAsync();
        var trade_19 = await api.Trade.GetOneClickRepayCurrenciesAsync(OkxTradeDebtType.Isolated);
        var trade_20 = await api.Trade.PlaceOneClickRepayOrderAsync([], "USDT");
        var trade_21 = await api.Trade.GetOneClickRepayHistoryAsync();
        var trade_22 = await api.Trade.MassCancelAsync(OkxInstrumentType.Option, "USDT");
        var trade_23 = await api.Trade.CancelAllAfterAsync(30, "TAG");
        var trade_24 = await api.Trade.GetAccountRateLimitAsync();
        var trade_25 = await api.Trade.CheckOrderAsync("BTC-USDT", OkxTradeMode.Cash, OkxTradeOrderSide.Buy, OkxTradePositionSide.Long, OkxTradeOrderType.MarketOrder, 0.1m);

        // Order Book Trading -> Algo Trading Methods (Signed)
        var algo_01 = await api.Algo.PlaceOrderAsync("BTC-USDT", OkxTradeMode.Isolated, OkxTradeOrderSide.Sell, OkxAlgoOrderType.Conditional);
        var algo_02 = await api.Algo.CancelOrderAsync(1_000_001L, "BTC-USDT");
        var algo_03 = await api.Algo.CancelOrdersAsync([]);
        var algo_04 = await api.Algo.AmendOrderAsync("BTC-USDT");
        var algo_05 = await api.Algo.CancelAdvanceAsync([]);
        var algo_06 = await api.Algo.GetOrderAsync(algoOrderId: 1_000_001);
        var algo_07 = await api.Algo.GetOpenOrdersAsync(OkxAlgoOrderType.OCO);
        var algo_08 = await api.Algo.GetOrderHistoryAsync(OkxAlgoOrderType.Conditional);

        // Order Book Trading -> Grid Trading Methods (Signed)
        var grid_01 = await api.Grid.PlaceOrderAsync(new OkxGridPlaceOrderRequest
        {
            InstrumentId = "BTC-USDT",
            AlgoOrderType = OkxGridAlgoOrderType.SpotGrid,
            MaximumPrice = 5000,
            MinimumPrice = 400,
            GridNumber = 10,
            GridRunType = OkxGridRunType.Arithmetic,
            QuoteSize = 25,
            TriggerParameters =
            [
                new()
                {
                    TriggerAction = OkxGridAlgoTriggerAction.Stop,
                    TriggerStrategy =  OkxGridTriggerStrategy.Price,
                    TriggerPrice = "1000"
                }
            ]
        });
        var grid_02 = await api.Grid.PlaceOrderAsync(
            instrumentId: "BTC-USDT-SWAP",
            algoOrderType: OkxGridAlgoOrderType.ContractGrid,
            maximumPrice: 5000,
            minimumPrice: 400,
            gridNumber: 10,
            gridRunType: OkxGridRunType.Arithmetic,
            size: 200,
            contractGridDirection: OkxGridContractDirection.Long,
            leverage: 2,
            triggerParameters:
            [
                new ()
                {
                    TriggerAction = OkxGridAlgoTriggerAction.Start,
                    TriggerStrategy =  OkxGridTriggerStrategy.RSI,
                    TimeFrame = OkxGridAlgoTimeFrame.ThirtyMinutes,
                    Threshold = "10",
                    TriggerCondition = OkxGridAlgoTriggerCondition.Cross,
                    TimePeriod = "14"
                },
                new ()
                {
                    TriggerAction = OkxGridAlgoTriggerAction.Stop,
                    TriggerStrategy =  OkxGridTriggerStrategy.Price,
                    TriggerPrice = "1000",
                    ContractAlgoStopType = OkxGridContractAlgoStopType.KeepPositions,
                }
            ]
        );
        var grid_03 = await api.Grid.AmendOrderAsync(new OkxGridOrderAmendRequest());
        var grid_04 = await api.Grid.AmendOrderAsync(448965992920907776, "BTC-USDT-SWAP", 1200);
        var grid_05 = await api.Grid.StopOrderAsync(448965992920907776, "BTC-USDT", OkxGridAlgoOrderType.SpotGrid, OkxGridSpotAlgoStopType.SellBaseCurrency);
        var grid_06 = await api.Grid.ClosePositionAsync(448965992920907776, true);
        var grid_07 = await api.Grid.CancelClosePositionAsync(448965992920907776, 570627699870375936);
        var grid_08 = await api.Grid.TriggerOrderAsync(448965992920907776);
        var grid_09 = await api.Grid.GetOpenOrdersAsync(OkxGridAlgoOrderType.SpotGrid);
        var grid_10 = await api.Grid.GetOrdersHistoryAsync(OkxGridAlgoOrderType.SpotGrid);
        var grid_11 = await api.Grid.GetOrderAsync(OkxGridAlgoOrderType.SpotGrid, 448965992920907776);
        var grid_12 = await api.Grid.GetSubOrdersAsync(OkxGridAlgoOrderType.SpotGrid, 448965992920907776, OkxGridAlgoSubOrderType.Live);
        var grid_13 = await api.Grid.GetPositionsAsync(OkxGridAlgoOrderType.ContractGrid, 448965992920907776);
        var grid_14 = await api.Grid.WithdrawIncomeAsync(448965992920907776);
        var grid_15 = await api.Grid.ComputeMarginBalanceAsync(448965992920907776, OkxAccountMarginAddReduce.Add, 10.0m);
        var grid_16 = await api.Grid.AdjustMarginBalanceAsync(448965992920907776, OkxAccountMarginAddReduce.Add, 10.0m);
        var grid_17 = await api.Grid.AddInvestmentAsync(448965992920907776, 1000.0m);
        var grid_18 = await api.Grid.GetAIParameterAsync(OkxGridAlgoOrderType.SpotGrid, "BTC-USDT");
        var grid_19 = await api.Grid.ComputeMinimumInvestmentAsync("ETH-USDT", OkxGridAlgoOrderType.SpotGrid, 5000, 3000, 50, OkxGridRunType.Arithmetic);
        var grid_20 = await api.Grid.RsiBacktestAsync("BTC-USDT", OkxGridAlgoTimeFrame.ThreeMinutes, 30, 14);

        // Order Book Trading -> Signal Bot Trading Methods (Signed)
        var signal_01 = await api.SignalBot.CreateChannelAsync("long short", "this is the first version");
        var signal_02 = await api.SignalBot.GetChannelsAsync(OkxSignalBotSourceType.FreeSignal);
        var signal_03 = await api.SignalBot.CreateSignalBotAsync(
            signalChannelId: 627921182788161536,
            instrumentIds: [
                "BTC-USDT-SWAP",
                "ETH-USDT-SWAP",
                "LTC-USDT-SWAP"
            ],
            leverage: 10,
            amount: 100.0m,
            orderType: OkxSignalBotOrderType.TradingView,
            entryParamaters: new OkxSignalBotEntryParamaters
            {
                AllowMultipleEntry = true,
                EntryType = OkxSignalBotEntryType.TradingViewSignal,
                Amount = "",
                Ratio = ""
            },
            exitParamaters: new OkxSignalBotExitParamaters
            {
                TakeProfitStopLossTriggerPrice = OkxSignalBotTriggerPrice.Price,
                TakeProfitPercentage = "",
                StopLossPercentage = ""
            });
        var signal_04 = await api.SignalBot.CancelSignalBotsAsync([627921182788161536]);

        // Order Book Trading -> Recurring Buy Methods (Signed)
        var recurring_01 = await api.RecurringBuy.PlaceOrderAsync("Strategy Name", new List<OkxRecurringBuyItem>(), OkxRecurringBuyPeriod.Monthly, 1, null, 1, "UTC", 1000.0m, "USDT", OkxTradeMode.Cross);
        var recurring_02 = await api.RecurringBuy.AmendOrderAsync(1_000_001, "Strategy Name");
        var recurring_03 = await api.RecurringBuy.StopOrderAsync(1_000_001);
        var recurring_04 = await api.RecurringBuy.GetOpenOrdersAsync();
        var recurring_05 = await api.RecurringBuy.GetOrderHistoryAsync();
        var recurring_06 = await api.RecurringBuy.GetOrderAsync(algoOrderId: 1_000_001);
        var recurring_07 = await api.RecurringBuy.GetSubOrdersAsync(algoOrderId: 1_000_001);

        // Order Book Trading -> Copy Trading Methods (Signed)
        var copy_01 = await api.CopyTrading.GetLeadingPositionsAsync();
        var copy_02 = await api.CopyTrading.GetLeadingPositionsHistoryAsync();
        var copy_03 = await api.CopyTrading.PlaceLeadingStopOrderAsync(positionId: 1_000_001);
        var copy_04 = await api.CopyTrading.CloseLeadingPositionAsync(positionId: 1_000_001);
        var copy_05 = await api.CopyTrading.GetLeadingInstrumentsAsync();
        var copy_06 = await api.CopyTrading.SetLeadingInstrumentsAsync(["BTC-USDT", "ETH-USDT"]);
        var copy_07 = await api.CopyTrading.GetProfitSharingDetailsAsync();
        var copy_08 = await api.CopyTrading.GetTotalProfitSharingAsync();
        var copy_09 = await api.CopyTrading.GetUnrealizedProfitSharingDetailsAsync();
        var copy_10 = await api.CopyTrading.GetTotalUnrealizedProfitSharingAsync();
        var copy_13 = await api.CopyTrading.AmendProfitSharingRatioAsync(1.0m);
        var copy_14 = await api.CopyTrading.GetAccountConfigurationAsync();
        var copy_15 = await api.CopyTrading.FirstCopySettingsAsync("---CODE---", OkxCopyTradingMarginMode.Copy, OkxCopyTradingInstrumentIdType.Copy, 1000.0M, OkxCopyTradingPositionCloseType.CopyClose);
        var copy_16 = await api.CopyTrading.AmendCopySettingsAsync("---CODE---", OkxCopyTradingMarginMode.Copy, OkxCopyTradingInstrumentIdType.Copy, 1000.0M, OkxCopyTradingPositionCloseType.CopyClose);
        var copy_17 = await api.CopyTrading.StopCopyingAsync("---CODE---", OkxCopyTradingPositionCloseType.CopyClose, OkxInstrumentType.Option);
        var copy_18 = await api.CopyTrading.GetCopySettingsAsync("---CODE---");
        var copy_23 = await api.CopyTrading.GetMyLeadTradersAsync();
        var copy_25 = await api.CopyTrading.GetPublicConfigurationAsync();
        var copy_26 = await api.CopyTrading.GetLeadTradersRanksAsync();
        var copy_27 = await api.CopyTrading.GetLeadTraderWeeklyPnlAsync("---CODE---");
        var copy_28 = await api.CopyTrading.GetLeadTraderDailyPnlAsync("---CODE---", "");
        var copy_29 = await api.CopyTrading.GetLeadTraderStatsAsync("---CODE---", "");
        var copy_30 = await api.CopyTrading.GetLeadTraderCurrencyPreferencesAsync("---CODE---");
        var copy_31 = await api.CopyTrading.GetLeadTraderCurrentPositionsAsync("---CODE---");
        var copy_32 = await api.CopyTrading.GetLeadTraderPositionHistoryAsync("---CODE---");
        var copy_33 = await api.CopyTrading.GetCopyTradersAsync("---CODE---");

        // Block Trading Methods (Unsigned)
        var block_01 = await api.Block.GetCounterpartiesAsync();
        var block_02 = await api.Block.CreateRfqAsync([], []);
        var block_03 = await api.Block.CancelRfqAsync();
        var block_04 = await api.Block.CancelRfqsAsync();
        var block_05 = await api.Block.CancelAllRfqsAsync();
        var block_06 = await api.Block.ExecuteQuoteAsync("", "");
        var block_07 = await api.Block.GetQuoteProductsAsync();
        var block_08 = await api.Block.SetQuoteProductsAsync([]);
        var block_09 = await api.Block.ResetMmpAsync();
        var block_10 = await api.Block.SetMmpAsync(30, 30, 30);
        var block_11 = await api.Block.GetMmpAsync();
        var block_12 = await api.Block.CreateQuoteAsync("", OkxTradeOrderSide.Buy, []);
        var block_13 = await api.Block.CancelQuoteAsync();
        var block_14 = await api.Block.CancelQuotesAsync();
        var block_15 = await api.Block.CancelAllQuotesAsync();
        var block_16 = await api.Block.CancelAllQuotesAfterAsync(30);
        var block_17 = await api.Block.GetRfqsAsync();
        var block_18 = await api.Block.GetQuotesAsync();
        var block_19 = await api.Block.GetTradesAsync();
        var block_20 = await api.Block.GetPublicExecutedTradesAsync();
        var block_21 = await api.Block.GetTickersAsync(OkxInstrumentType.Futures);
        var block_22 = await api.Block.GetTickerAsync("");
        var block_23 = await api.Block.GetPublicRecentTradesAsync("");

        // Spread Trading Methods (Signed)
        var spread_01 = await api.Spread.PlaceOrderAsync("", OkxTradeOrderSide.Buy, OkxSpreadOrderType.LimitOrder, 1.0m);
        var spread_02 = await api.Spread.CancelOrderAsync();
        var spread_03 = await api.Spread.CancelOrdersAsync("");
        var spread_04 = await api.Spread.AmendOrderAsync();
        var spread_05 = await api.Spread.GetOrderAsync();
        var spread_06 = await api.Spread.GetOpenOrdersAsync();
        var spread_07 = await api.Spread.GetOrderHistoryAsync();
        var spread_08 = await api.Spread.GetOrderArchiveAsync();
        var spread_09 = await api.Spread.GetTradesAsync();
        var spread_10 = await api.Spread.GetSpreadsAsync();
        var spread_11 = await api.Spread.GetOrderBookAsync("");
        var spread_12 = await api.Spread.GetTickerAsync("");
        var spread_13 = await api.Spread.GetTradesAsync();
        var spread_14 = await api.Spread.GetCandlesticksAsync("", OkxPeriod.OneDay);
        var spread_15 = await api.Spread.GetCandlesticksHistoryAsync("", OkxPeriod.OneDay);
        var spread_16 = await api.Spread.CancelAllAfterAsync(30);

        // Trading Statistics Methods (Unsigned)
        var rubik_01 = await api.Rubik.GetSupportCoinAsync();
        var rubik_02 = await api.Rubik.GetContractOpenInterestHistoryAsync("");
        var rubik_03 = await api.Rubik.GetTakerVolumeAsync("BTC", OkxInstrumentType.Spot);
        var rubik_04 = await api.Rubik.GetContractTakerVolumeAsync("");
        var rubik_05 = await api.Rubik.GetMarginLendingRatioAsync("BTC", "1D");
        var rubik_06 = await api.Rubik.GetTopTradersContractLongShortRatioAsync("");
        var rubik_07 = await api.Rubik.GetTopTradersContractLongShortRatioByPositionAsync("");
        var rubik_08 = await api.Rubik.GetContractLongShortRatioAsync("");
        var rubik_09 = await api.Rubik.GetLongShortRatioAsync("BTC", "1D");
        var rubik_10 = await api.Rubik.GetContractSummaryAsync("BTC", "1D");
        var rubik_11 = await api.Rubik.GetOptionsSummaryAsync("BTC", "1D");
        var rubik_12 = await api.Rubik.GetPutCallRatioAsync("BTC", "1D");
        var rubik_13 = await api.Rubik.GetInterestVolumeExpiryAsync("BTC", "1D");
        var rubik_14 = await api.Rubik.GetInterestVolumeStrikeAsync("BTC", "20210623", "1D");
        var rubik_15 = await api.Rubik.GetTakerFlowAsync("BTC", "1D");

        // Funding Account Methods (Signed)
        var funding_01 = await api.Funding.GetCurrenciesAsync();
        var funding_02 = await api.Funding.GetBalancesAsync();
        var funding_03 = await api.Funding.GetNonTradableBalancesAsync();
        var funding_04 = await api.Funding.GetAssetValuationAsync();
        var funding_05 = await api.Funding.FundTransferAsync(OkxFundingTransferType.TransferWithinAccount, "BTC", 0.5m, OkxAccount.Funding, OkxAccount.Trading);
        var funding_06 = await api.Funding.FundTransferStateAsync();
        var funding_07 = await api.Funding.GetFundingBillDetailsAsync("BTC");
        var funding_09 = await api.Funding.GetDepositAddressAsync("BTC");
        var funding_10 = await api.Funding.GetDepositAddressAsync("USDT");
        var funding_12 = await api.Funding.GetDepositHistoryAsync("USDT");
        var funding_13 = await api.Funding.WithdrawAsync("USDT", 100.0m, OkxFundingWithdrawalDestination.DigitalCurrencyAddress, "toAddress", 1.0m, "USDT-TRC20");
        var funding_15 = await api.Funding.CancelWithdrawalAsync(1_000_001);
        var funding_16 = await api.Funding.GetWithdrawalHistoryAsync("USDT");
        var funding_17 = await api.Funding.GetDepositStatusAsync("", "", "", "");
        var funding_18 = await api.Funding.GetWithdrawalStatusAsync(1_000_000L);
        var funding_19 = await api.Funding.ConvertDustAssetsAsync([]);
        var funding_20 = await api.Funding.GetExchangeListAsync();
        var funding_21 = await api.Funding.ApplyForMonthlyStatementAsync();
        var funding_22 = await api.Funding.GetMonthlyStatementAsync("");
        var funding_23 = await api.Funding.GetConvertCurrenciesAsync();
        var funding_24 = await api.Funding.GetConvertCurrencyPairAsync("", "");
        var funding_25 = await api.Funding.EstimateQuoteAsync("", "", OkxTradeOrderSide.Buy, 1000.0M, "");
        var funding_26 = await api.Funding.PlaceConvertOrderAsync("", "", "", OkxTradeOrderSide.Buy, 1000.0M, "");
        var funding_27 = await api.Funding.GetConvertHistoryAsync();

        // Sub-Account Methods (Signed)
        var subaccount_01 = await api.SubAccount.GetSubAccountsAsync();
        var subaccount_02 = await api.SubAccount.ResetSubAccountApiKeyAsync("subAccountName", "apiKey", "apiLabel", true, true, "");
        var subaccount_03 = await api.SubAccount.GetSubAccountTradingBalancesAsync("subAccountName");
        var subaccount_04 = await api.SubAccount.GetSubAccountFundingBalancesAsync("subAccountName");
        var subaccount_05 = await api.SubAccount.GetSubAccountMaximumWithdrawalsAsync("subAccountName");
        var subaccount_06 = await api.SubAccount.GetSubAccountBillsAsync();
        var subaccount_07 = await api.SubAccount.GetSubAccountManagedBillsAsync();
        var subaccount_08 = await api.SubAccount.TransferBetweenSubAccountsAsync("BTC", 0.5m, OkxAccount.Funding, OkxAccount.Trading, "fromSubAccountName", "toSubAccountName");
        var subaccount_09 = await api.SubAccount.SetPermissionOfTransferOutAsync("", true);
        var subaccount_10 = await api.SubAccount.GetCustodySubAccountsAsync("");
        var subaccount_11 = await api.SubAccount.SetLoanAllocationAsync(false, []);
        var subaccount_12 = await api.SubAccount.GetInterestLimitsAsync("");

        // Financial Product -> On-Chain Earn Methods (Signed)
        var onchain_01 = await api.Financial.OnChainEarn.GetOffersAsync();
        var onchain_02 = await api.Financial.OnChainEarn.PurchaseAsync("", []);
        var onchain_03 = await api.Financial.OnChainEarn.RedeemAsync("", "");
        var onchain_04 = await api.Financial.OnChainEarn.CancelAsync("", "");
        var onchain_05 = await api.Financial.OnChainEarn.GetOpenOrdersAsync();
        var onchain_06 = await api.Financial.OnChainEarn.GetHistoryAsync();

        // Financial Product -> ETH Staking Methods (Signed)
        var staking_01 = await api.Financial.EthStaking.PurchaseAsync(1.0m);
        var staking_02 = await api.Financial.EthStaking.RedeemAsync(1.0m);
        var staking_03 = await api.Financial.EthStaking.GetBalancesAsync();
        var staking_04 = await api.Financial.EthStaking.GetHistoryAsync(OkxFinancialEthStakingType.Purchase);
        var staking_05 = await api.Financial.EthStaking.GetApyHistoryAsync(30);

        // Financial Product -> Simple Earn Flexible Methods (Signed)
        var flexible_01 = await api.Financial.FlexibleSimpleEarn.GetBalancesAsync();
        var flexible_02 = await api.Financial.FlexibleSimpleEarn.PurchaseAsync("", 1_000.0m, 1.0m);
        var flexible_03 = await api.Financial.FlexibleSimpleEarn.RedeemAsync("", 1_000.0m, 1.0m);
        var flexible_04 = await api.Financial.FlexibleSimpleEarn.SetLendingRateAsync("", 1.0m);
        var flexible_05 = await api.Financial.FlexibleSimpleEarn.GetLendingHistoryAsync();
        var flexible_06 = await api.Financial.FlexibleSimpleEarn.GetPublicBorrowSummaryAsync();
        var flexible_07 = await api.Financial.FlexibleSimpleEarn.GetPublicBorrowHistoryAsync();

        // Affiliate Methods (Signed)
        var affiliate_01 = await api.Affiliate.GetInviteeAsync(1_000_000L);

        // TODO: NDBroker Methods (Signed)
        // TODO: FDBroker Methods (Signed)
        #endregion

        #region WebSocket Api Client Examples
        // OKX Socket Client
        var ws = new OkxWebSocketApiClient();
        ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

        // Subscription
        var subscription = await ws.Public.SubscribeToTickersAsync((data) =>
           {
               // ... Your logic here
               Console.WriteLine($"Ticker {data.InstrumentId} Ask:{data.AskPrice} Bid:{data.BidPrice}");
           }, "BTC-USDT");

        // UnSubscription
        _ = ws.UnsubscribeAsync(subscription.Data);

        // Public Updates --------------------------------------------------------------------------------
        await ws.Public.SubscribeToInstrumentsAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Spot);
        await ws.Public.SubscribeToOpenInterestsAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT");
        await ws.Public.SubscribeToFundingRatesAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT");
        await ws.Public.SubscribeToPriceLimitAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT");
        await ws.Public.SubscribeToOptionSummaryAsync((data) =>
        {
            // ... Your logic here
        }, "USD");
        await ws.Public.SubscribeToEstimatedPriceAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Option);
        await ws.Public.SubscribeToMarkPriceAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT");
        await ws.Public.SubscribeToIndexTickersAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT");
        await ws.Public.SubscribeToMarkPriceCandlesticksAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT", OkxPeriod.FiveMinutes);
        await ws.Public.SubscribeToIndexCandlesticksAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT", OkxPeriod.FiveMinutes);

        var pairs = new List<string> {
            "BTC-USDT", "LTC-USDT", "ETH-USDT", "XRP-USDT", "BCH-USDT",
            "EOS-USDT", "OKB-USDT", "ETC-USDT", "TRX-USDT", "BSV-USDT",
            "DASH-USDT", "NEO-USDT", "QTUM-USDT", "XLM-USDT", "ADA-USDT",
            "AE-USDT", "BLOC-USDT", "EGT-USDT", "IOTA-USDT", "SC-USDT",
            "WXT-USDT", "ZEC-USDT",
        };
        foreach (var pair in pairs)
        {
            await ws.Public.SubscribeToTickersAsync((data) =>
            {
                // ... Your logic here
                Console.WriteLine($"Ticker {data.InstrumentId} Ask:{data.AskPrice} Bid:{data.BidPrice}");
            }, pair);
        }
        await ws.Public.SubscribeToCandlesticksAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT", OkxPeriod.FiveMinutes);
        await ws.Public.SubscribeToTradesAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT");
        await ws.Public.SubscribeToOrderBookAsync((data) =>
        {
            // ... Your logic here
        }, "BTC-USDT", OkxOrderBookType.OrderBook);
        await ws.Public.SubscribeToSystemUpgradeStatusAsync((data) =>
        {
            // ... Your logic here
        });
        // End of Public Updates -------------------------------------------------------------------


        // TradingAccount Updates
        await ws.Account.SubscribeToAccountUpdatesAsync((data) =>
        {
            // ... Your logic here
        });
        await ws.Account.SubscribeToPositionUpdatesAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");
        await ws.Account.SubscribeToBalanceAndPositionUpdatesAsync((data) =>
        {
            // ... Your logic here
        });
        await ws.Account.SubscribeToPositionRiskUpdatesAsync(OkxInstrumentType.Futures, (data) =>
        {
            // ... Your logic here
        });
        await ws.Account.SubscribeToAccountGreeksUpdatesAsync((data) =>
        {
            // ... Your logic here
        });
        // End of TradingAccount Updates -----------------------------------------------------------

        // TODO: FundingAccount Updates

        // Trade Updates (Private) -----------------------------------------------------------------
        await ws.Trade.SubscribeToOrderUpdatesAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");
        await ws.Trade.SubscribeToFillsAsync((data) =>
        {
            // ... Your logic here
        });
        await ws.Trade.PlaceOrderAsync(new OkxTradeOrderPlaceRequest());
        await ws.Trade.PlaceOrdersAsync(new List<OkxTradeOrderPlaceRequest>());
        await ws.Trade.CancelOrderAsync(new OkxTradeOrderCancelRequest());
        await ws.Trade.CancelOrdersAsync(new List<OkxTradeOrderCancelRequest>());
        await ws.Trade.AmendOrderAsync(new OkxTradeOrderAmendRequest());
        await ws.Trade.AmendOrdersAsync(new List<OkxTradeOrderAmendRequest>());
        // End of Trade Updates (Private) ----------------------------------------------------------

        // AlgoTrading Updates (Private) -----------------------------------------------------------
        await ws.Algo.SubscribeToAlgoOrderUpdatesAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");
        await ws.Algo.SubscribeToAdvanceAlgoOrderUpdatesAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");
        // End of AlgoTrading Updates (Private) ----------------------------------------------------

        // TODO: GridTrading Updates (Private)

        // RecurringBuy Updates (Private) ----------------------------------------------------------
        await ws.RecurringBuy.SubscribeToOrderUpdatesAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Spot);
        // End of RecurringBuy Updates (Private) ---------------------------------------------------

        // TODO: CopyTrading Updates (Private)
        // TODO: BlockTrading Updates (Private)
        // TODO: SpreadTrading Updates (Private)
        #endregion
    }
}