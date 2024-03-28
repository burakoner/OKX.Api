using OKX.Api.RecurringBuy.Enums;
using OKX.Api.RecurringBuy.Models;

namespace OKX.Api.Examples;

internal class Program
{
    static async Task Main(string[] args)
    {
        #region Rest Api Client Examples
        var api = new OkxRestApiClient(new OkxRestApiOptions
        {
            RawResponse = true,
        });
        api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

        // Public & Market & System Methods (Unsigned)
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
        var public_20 = await api.Public.GetPositionTiersAsync(OkxInstrumentType.Futures, OkxMarginMode.Isolated, "BTC-USD");
        var public_21 = await api.Public.GetInterestRatesAsync();
        var public_22 = await api.Public.GetVIPInterestRatesAsync();
        var public_23 = await api.Public.GetUnderlyingAsync(OkxInstrumentType.Futures);
        var public_24 = await api.Public.GetUnderlyingAsync(OkxInstrumentType.Option);
        var public_25 = await api.Public.GetUnderlyingAsync(OkxInstrumentType.Swap);
        var public_26 = await api.Public.GetInsuranceFundAsync(OkxInstrumentType.Margin, currency: "BTC");
        var public_27 = await api.Public.GetUnitConvertAsync("BTC-USD-SWAP", price: 35000, size: 0.888m);
        var public_28 = await api.Public.GetIndexTickersAsync(instrumentId: "BTC-USDT");
        var public_29 = await api.Public.GetIndexCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_30 = await api.Public.GetIndexCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_31 = await api.Public.GetMarkPriceCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_32 = await api.Public.GetMarkPriceCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_33 = await api.Public.GetOracleAsync();
        var public_34 = await api.Public.GetExchangeRatesAsync();
        var public_35 = await api.Public.GetIndexComponentsAsync("BTC-USDT");
        var public_36 = await api.Public.GetEconomicCalendarDataAsync("BTC-USDT");
        var market_37 = await api.Public.GetTickersAsync(OkxInstrumentType.Spot);
        var market_38 = await api.Public.GetTickerAsync("BTC-USDT");
        var market_39 = await api.Public.GetOrderBookAsync("BTC-USDT", 40);
        var market_40 = await api.Public.GetOrderBookFullAsync("BTC-USDT", 5000);
        var market_41 = await api.Public.GetCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var market_42 = await api.Public.GetCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        var market_43 = await api.Public.GetTradesAsync("BTC-USDT");
        var market_44 = await api.Public.GetTradesHistoryAsync("BTC-USDT");
        var market_45 = await api.Public.GetOptionTradesByInstrumentFamilyAsync("BTC-USDT");
        var market_46 = await api.Public.GetOptionTradesAsync("BTC-USDT");
        var market_47 = await api.Public.Get24HourVolumeAsync();
        var market_48 = await api.Public.GetSystemUpgradeStatusAsync();

        // Trading Account Methods (Signed)
        var account_01 = await api.Account.GetBalancesAsync();
        var account_02 = await api.Account.GetPositionsAsync();
        var account_03 = await api.Account.GetPositionsHistoryAsync();
        var account_04 = await api.Account.GetPositionRiskAsync();
        var account_05 = await api.Account.GetBillHistoryAsync();
        var account_06 = await api.Account.GetBillArchiveAsync();
        var account_07 = await api.Account.GetConfigurationAsync();
        var account_08 = await api.Account.SetPositionModeAsync(OkxPositionMode.LongShortMode);
        var account_09 = await api.Account.GetLeverageAsync("BTC-USD-211008", OkxMarginMode.Isolated);
        var account_10 = await api.Account.SetLeverageAsync(30, null, "BTC-USD-211008", OkxMarginMode.Isolated, OkxPositionSide.Long);
        var account_11 = await api.Account.GetMaximumAmountAsync("BTC-USDT", OkxTradeMode.Isolated);
        var account_12 = await api.Account.GetMaximumAvailableAmountAsync("BTC-USDT", OkxTradeMode.Isolated);
        var account_13 = await api.Account.SetMarginAmountAsync("BTC-USDT", OkxPositionSide.Long, OkxMarginAddReduce.Add, 100.0m);
        var account_14 = await api.Account.GetLeverageEstimatedInformationAsync(OkxInstrumentType.Futures, OkxMarginMode.Cross, 10);
        var account_15 = await api.Account.GetMaximumLoanAmountAsync("BTC-USDT", OkxMarginMode.Cross);
        var account_16 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Spot);
        var account_17 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Futures);
        var account_18 = await api.Account.GetInterestAccruedAsync();
        var account_19 = await api.Account.GetInterestRateAsync();
        var account_20 = await api.Account.SetGreeksAsync(OkxGreeksType.GreeksInCoins);
        var account_21 = await api.Account.SetIsolatedMarginModeAsync(OkxInstrumentType.Futures, OkxIsolatedMarginMode.AutoTransfer);
        var account_22 = await api.Account.GetMaximumWithdrawalsAsync();
        var account_23 = await api.Account.GetRiskStateAsync();
        var account_24 = await api.Account.QuickMarginBorrowAsync("BTC-USDT", "BTC", 0.1m);
        var account_25 = await api.Account.QuickMarginRepayAsync("BTC-USDT", "BTC", 0.1m);
        var account_26 = await api.Account.GetQuickMarginBorrowRepayHistoryAsync();
        var account_27 = await api.Account.VipLoanBorrowAsync("USDT", 1000.0m);
        var account_28 = await api.Account.VipLoanRepayAsync("USDT", 1000.0m, 1_000_001);
        var account_29 = await api.Account.GetVipLoanBorrowRepayHistoryAsync();
        var account_30 = await api.Account.GetVipLoanBorrowRepayHistoryAsync("USDT");
        var account_31 = await api.Account.GetVipInterestAccruedDataAsync();
        var account_32 = await api.Account.GetVipInterestAccruedDataAsync("USDT");
        var account_33 = await api.Account.GetVipInterestDeductedDataAsync();
        var account_34 = await api.Account.GetVipInterestDeductedDataAsync("USDT");
        var account_35 = await api.Account.GetVipLoanOrdersAsync();
        var account_36 = await api.Account.GetVipLoanOrderDetailsAsync();
        var account_37 = await api.Account.GetInterestLimitsAsync();
        var account_38 = await api.Account.SimulateMarginAsync();
        var account_39 = await api.Account.PositionBuilderAsync();
        var account_40 = await api.Account.GetGreeksAsync();
        var account_41 = await api.Account.GetPositionTiersAsync(OkxInstrumentType.Futures);
        var account_42 = await api.Account.SetRiskOffsetTypeAsync(OkxDerivativesOffsetMode.UsdtDerivatives);
        var account_43 = await api.Account.ActivateOptionAsync();
        var account_44 = await api.Account.SetAutoLoanAsync(true);
        var account_45 = await api.Account.SetLevelAsync(OkxAccountLevel.Simple);
        var account_46 = await api.Account.ResetMmpAsync("BTC-USDT");
        var account_47 = await api.Account.SetMmpConfigurationAsync("BTC-USDT", 5000, 5000, 1);
        var account_48 = await api.Account.GetMmpConfigurationAsync("BTC-USDT");

        // FundingAccount Methods (Signed)
        var funding_01 = await api.Funding.GetCurrenciesAsync();
        var funding_02 = await api.Funding.GetBalancesAsync();
        var funding_03 = await api.Funding.GetNonTradableBalancesAsync();
        var funding_04 = await api.Funding.GetAssetValuationAsync();
        var funding_05 = await api.Funding.FundTransferAsync("BTC", 0.5m, OkxTransferType.TransferWithinAccount, OkxAccount.Funding, OkxAccount.Trading);
        var funding_06 = await api.Funding.GetFundingBillDetailsAsync("BTC");
        var funding_07 = await api.Funding.GetLightningDepositsAsync("BTC", 0.001m);
        var funding_08 = await api.Funding.GetDepositAddressAsync("BTC");
        var funding_09 = await api.Funding.GetDepositAddressAsync("USDT");
        var funding_10 = await api.Funding.GetDepositHistoryAsync("USDT");
        var funding_11 = await api.Funding.WithdrawAsync("USDT", 100.0m, OkxWithdrawalDestination.DigitalCurrencyAddress, "toAddress", 1.0m, "USDT-TRC20");
        var funding_12 = await api.Funding.GetLightningWithdrawalsAsync("BTC", "invoice", "password");
        var funding_13 = await api.Funding.CancelWithdrawalAsync(1_000_001);
        var funding_14 = await api.Funding.GetWithdrawalHistoryAsync("USDT");

        // SubAccount Methods (Signed)
        var subaccount_01 = await api.SubAccount.GetSubAccountsAsync();
        var subaccount_02 = await api.SubAccount.ResetSubAccountApiKeyAsync("subAccountName", "apiKey", "apiLabel", true, true, "");
        var subaccount_03 = await api.SubAccount.GetSubAccountTradingBalancesAsync("subAccountName");
        var subaccount_04 = await api.SubAccount.GetSubAccountFundingBalancesAsync("subAccountName");
        var subaccount_05 = await api.SubAccount.GetSubAccountMaximumWithdrawalsAsync("subAccountName");
        var subaccount_06 = await api.SubAccount.GetSubAccountBillsAsync();
        var subaccount_07 = await api.SubAccount.GetManagedSubAccountBillsAsync();
        var subaccount_08 = await api.SubAccount.TransferBetweenSubAccountsAsync("BTC", 0.5m, OkxAccount.Funding, OkxAccount.Trading, "fromSubAccountName", "toSubAccountName");

        // Trade Methods (Signed)
        var trade_01 = await api.Trading.PlaceOrderAsync("BTC-USDT", OkxTradeMode.Cash, OkxOrderSide.Buy, OkxPositionSide.Long, OkxOrderType.MarketOrder, 0.1m);
        var trade_02 = await api.Trading.PlaceOrdersAsync([]);
        var trade_03 = await api.Trading.CancelOrderAsync("BTC-USDT");
        var trade_04 = await api.Trading.CancelOrdersAsync([]);
        var trade_05 = await api.Trading.AmendOrderAsync("BTC-USDT");
        var trade_06 = await api.Trading.AmendOrdersAsync([]);
        var trade_07 = await api.Trading.ClosePositionAsync("BTC-USDT", OkxMarginMode.Isolated);
        var trade_08 = await api.Trading.GetOrderAsync("BTC-USDT");
        var trade_09 = await api.Trading.GetOpenOrdersAsync();
        var trade_10 = await api.Trading.GetOrderHistoryAsync(OkxInstrumentType.Swap);
        var trade_11 = await api.Trading.GetOrderArchiveAsync(OkxInstrumentType.Futures);
        var trade_12 = await api.Trading.GetTradesAsync();
        var trade_13 = await api.Trading.GetTradesHistoryAsync(OkxInstrumentType.Futures);
        var trade_14 = await api.Trading.ApplyTradesArchiveAsync(2023, OkxQuarter.Quarter1);
        var trade_15 = await api.Trading.GetTradesArchiveAsync(2023, OkxQuarter.Quarter1);

        // AlgoTrading Methods (Signed)
        var algo_01 = await api.AlgoTrading.PlaceOrderAsync("BTC-USDT", OkxTradeMode.Isolated, OkxOrderSide.Sell, OkxAlgoOrderType.Conditional);
        var algo_02 = await api.AlgoTrading.CancelOrderAsync([]);
        var algo_03 = await api.AlgoTrading.AmendOrderAsync("BTC-USDT");
        var algo_04 = await api.AlgoTrading.CancelAdvanceAsync([]);
        var algo_05 = await api.AlgoTrading.GetOrderAsync(algoOrderId: 1_000_001);
        var algo_06 = await api.AlgoTrading.GetOpenOrdersAsync(OkxAlgoOrderType.OCO);
        var algo_07 = await api.AlgoTrading.GetOrderHistoryAsync(OkxAlgoOrderType.Conditional);

        // GridTrading Methods (Signed)
        var grid_01 = await api.GridTrading.PlaceOrderAsync(new OkxGridPlaceOrderRequest
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
                new OkxGridPlaceTriggerParameters
                {
                    TriggerAction = OkxGridAlgoTriggerAction.Stop,
                    TriggerStrategy =  OkxGridAlgoTriggerStrategy.Price,
                    TriggerPrice = "1000"
                }
            ]
        });
        var grid_02 = await api.GridTrading.PlaceOrderAsync(
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
                new OkxGridPlaceTriggerParameters
                {
                    TriggerAction = OkxGridAlgoTriggerAction.Start,
                    TriggerStrategy =  OkxGridAlgoTriggerStrategy.Rsi,
                    TimeFrame = OkxGridAlgoTimeFrame.ThirtyMinutes,
                    Threshold = "10",
                    TriggerCondition = OkxGridAlgoTriggerCondition.Cross,
                    TimePeriod = "14"
                },
                new OkxGridPlaceTriggerParameters
                {
                    TriggerAction = OkxGridAlgoTriggerAction.Stop,
                    TriggerStrategy =  OkxGridAlgoTriggerStrategy.Price,
                    TriggerPrice = "1000",
                    ContractAlgoStopType = OkxGridContractAlgoStopType.KeepPositions,
                }
            ]
        );
        var grid_03 = await api.GridTrading.AmendOrderAsync(new OkxGridAmendOrderRequest());
        var grid_04 = await api.GridTrading.AmendOrderAsync(448965992920907776, "BTC-USDT-SWAP", 1200);
        var grid_05 = await api.GridTrading.StopOrderAsync(448965992920907776, "BTC-USDT", OkxGridAlgoOrderType.SpotGrid, OkxGridSpotAlgoStopType.SellBaseCurrency);
        var grid_06 = await api.GridTrading.ClosePositionAsync(448965992920907776, true);
        var grid_07 = await api.GridTrading.CancelClosePositionAsync(448965992920907776, 570627699870375936);
        var grid_08 = await api.GridTrading.TriggerOrderAsync(448965992920907776);
        var grid_09 = await api.GridTrading.GetOpenOrdersAsync(OkxGridAlgoOrderType.SpotGrid);
        var grid_10 = await api.GridTrading.GetOrdersHistoryAsync(OkxGridAlgoOrderType.SpotGrid);
        var grid_11 = await api.GridTrading.GetOrderAsync(OkxGridAlgoOrderType.SpotGrid, 448965992920907776);
        var grid_12 = await api.GridTrading.GetSubOrdersAsync(OkxGridAlgoOrderType.SpotGrid, 448965992920907776, OkxGridAlgoSubOrderType.Live);
        var grid_13 = await api.GridTrading.GetPositionsAsync(OkxGridAlgoOrderType.ContractGrid, 448965992920907776);
        var grid_14 = await api.GridTrading.GetWithdrawIncomeAsync(448965992920907776);
        var grid_15 = await api.GridTrading.ComputeMarginBalanceAsync(448965992920907776, OkxMarginAddReduce.Add, 10.0m);
        var grid_16 = await api.GridTrading.AdjustMarginBalanceAsync(448965992920907776, OkxMarginAddReduce.Add, 10.0m);
        var grid_17 = await api.GridTrading.GetAIParameterAsync(OkxGridAlgoOrderType.SpotGrid, "BTC-USDT");
        var grid_18 = await api.GridTrading.ComputeMinimumInvestmentAsync("ETH-USDT", OkxGridAlgoOrderType.SpotGrid, 5000, 3000, 50, OkxGridRunType.Arithmetic);
        var grid_19 = await api.GridTrading.RsiBackTestingAsync("BTC-USDT", OkxGridAlgoTimeFrame.ThreeMinutes, 30, 14);

        // TODO: SignalTrading Methods (Signed)

        // RecurringBuy Methods (Signed)
        var recurring_01 = await api.RecurringBuy.PlaceOrderAsync("Strategy Name", new List<OkxRecurringItem>(), OkxRecurringBuyPeriod.Monthly, 1, null, 1, "UTC", 1000.0m, "USDT", OkxTradeMode.Cross);
        var recurring_02 = await api.RecurringBuy.AmendOrderAsync(1_000_001, "Strategy Name");
        var recurring_03 = await api.RecurringBuy.StopOrderAsync(1_000_001);
        var recurring_04 = await api.RecurringBuy.GetOpenOrdersAsync();
        var recurring_05 = await api.RecurringBuy.GetOrderHistoryAsync();
        var recurring_06 = await api.RecurringBuy.GetOrderAsync(algoOrderId: 1_000_001);
        var recurring_07 = await api.RecurringBuy.GetSubOrdersAsync(algoOrderId: 1_000_001);

        // CopyTrading Methods (Signed)
        var copy_01 = await api.CopyTrading.GetLeadingPositionsAsync();
        var copy_02 = await api.CopyTrading.GetLeadingPositionsHistoryAsync();
        var copy_03 = await api.CopyTrading.PlaceLeadingStopOrderAsync(leadingPositionId: 1_000_001);
        var copy_04 = await api.CopyTrading.CloseLeadingPositionAsync(leadingPositionId: 1_000_001);
        var copy_05 = await api.CopyTrading.GetLeadingInstrumentsAsync();
        var copy_06 = await api.CopyTrading.SetLeadingInstrumentsAsync(["BTC-USDT", "ETH-USDT"]);
        var copy_07 = await api.CopyTrading.GetProfitSharingDetailsAsync();
        var copy_08 = await api.CopyTrading.GetProfitSharingTotalAsync();
        var copy_09 = await api.CopyTrading.GetUnrealizedProfitSharingDetailsAsync();

        // BlockTrading Methods (Unsigned)
        var block_01 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Spot);
        var block_02 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Futures);
        var block_03 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Option);
        var block_04 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Swap);
        var block_05 = await api.BlockTrading.GetBlockTickerAsync("BTC-USDT");
        var block_06 = await api.BlockTrading.GetBlockTradesAsync("BTC-USDT");

        // TODO: SpreadTrading Methods (Signed)
        // TODO: Earn Methods (Signed)
        // TODO: Saving Methods (Signed)
        // TODO: Staking Methods (Signed)

        // TradingStatistics Methods (Unsigned)
        var rubik_01 = await api.Rubik.GetSupportCoinAsync();
        var rubik_02 = await api.Rubik.GetTakerVolumeAsync("BTC", OkxInstrumentType.Spot);
        var rubik_03 = await api.Rubik.GetMarginLendingRatioAsync("BTC", OkxPeriod.OneDay);
        var rubik_04 = await api.Rubik.GetLongShortRatioAsync("BTC", OkxPeriod.OneDay);
        var rubik_05 = await api.Rubik.GetContractSummaryAsync("BTC", OkxPeriod.OneDay);
        var rubik_06 = await api.Rubik.GetOptionsSummaryAsync("BTC", OkxPeriod.OneDay);
        var rubik_07 = await api.Rubik.GetPutCallRatioAsync("BTC", OkxPeriod.OneDay);
        var rubik_08 = await api.Rubik.GetInterestVolumeExpiryAsync("BTC", OkxPeriod.OneDay);
        var rubik_09 = await api.Rubik.GetInterestVolumeStrikeAsync("BTC", "20210623", OkxPeriod.OneDay);
        var rubik_10 = await api.Rubik.GetTakerFlowAsync("BTC", OkxPeriod.OneDay);

        // TODO: Affiliate Methods (Signed)
        // TODO: NDBroker Methods (Signed)
        // TODO: FDBroker Methods (Signed)
        #endregion

        #region WebSocket Api Client Examples
        // OKX Socket Client
        var ws = new OKXWebSocketApiClient();
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

        var pairs = new List<string> { "BTC-USDT", "LTC-USDT", "ETH-USDT", "XRP-USDT", "BCH-USDT", "EOS-USDT", "OKB-USDT", "ETC-USDT", "TRX-USDT", "BSV-USDT", "DASH-USDT", "NEO-USDT", "QTUM-USDT", "XLM-USDT", "ADA-USDT", "AE-USDT", "BLOC-USDT", "EGT-USDT", "IOTA-USDT", "SC-USDT", "WXT-USDT", "ZEC-USDT", };
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
        await ws.Trading.SubscribeToOrderUpdatesAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");

        await ws.Trading.PlaceOrderAsync(new Trade.Models.OkxOrderPlaceRequest());
        await ws.Trading.PlaceOrdersAsync(new List<Trade.Models.OkxOrderPlaceRequest>());
        await ws.Trading.CancelOrderAsync(new Trade.Models.OkxOrderCancelRequest());
        await ws.Trading.CancelOrdersAsync(new List<Trade.Models.OkxOrderCancelRequest>());
        await ws.Trading.AmendOrderAsync(new Trade.Models.OkxOrderAmendRequest());
        await ws.Trading.AmendOrdersAsync(new List<Trade.Models.OkxOrderAmendRequest>());
        // End of Trade Updates (Private) ----------------------------------------------------------

        // AlgoTrading Updates (Private) -----------------------------------------------------------
        await ws.AlgoTrading.SubscribeToAlgoOrderUpdatesAsync((data) =>
        {
            // ... Your logic here
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");
        await ws.AlgoTrading.SubscribeToAdvanceAlgoOrderUpdatesAsync((data) =>
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