using OKX.Api.Account.Enums;
using OKX.Api.AlgoTrading.Enums;
using OKX.Api.Common.Enums;
using OKX.Api.Funding.Enums;
using OKX.Api.GridTrading.Enums;
using OKX.Api.GridTrading.Models;
using OKX.Api.Public.Enums;
using OKX.Api.Trade.Enums;

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
        var account_14 = await api.Account.GetMaximumLoanAmountAsync("BTC-USDT", OkxMarginMode.Cross);
        var account_15 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Spot);
        var account_16 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Futures);
        var account_17 = await api.Account.GetInterestAccruedAsync();
        var account_18 = await api.Account.GetInterestRateAsync();
        var account_19 = await api.Account.SetGreeksAsync(OkxGreeksType.GreeksInCoins);
        var account_20 = await api.Account.GetMaximumWithdrawalsAsync();

        // Trade Methods (Signed)
        var trade_01 = await api.Trade.PlaceOrderAsync("BTC-USDT", OkxTradeMode.Cash, OkxOrderSide.Buy, OkxPositionSide.Long, OkxOrderType.MarketOrder, 0.1m);
        var trade_02 = await api.Trade.PlaceOrdersAsync([]);
        var trade_03 = await api.Trade.CancelOrderAsync("BTC-USDT");
        var trade_04 = await api.Trade.CancelOrdersAsync([]);
        var trade_05 = await api.Trade.AmendOrderAsync("BTC-USDT");
        var trade_06 = await api.Trade.AmendOrdersAsync([]);
        var trade_07 = await api.Trade.ClosePositionAsync("BTC-USDT", OkxMarginMode.Isolated);
        var trade_08 = await api.Trade.GetOrderAsync("BTC-USDT");
        var trade_09 = await api.Trade.GetOrdersAsync();
        var trade_10 = await api.Trade.GetOrderHistoryAsync(OkxInstrumentType.Swap);
        var trade_11 = await api.Trade.GetOrderArchiveAsync(OkxInstrumentType.Futures);
        var trade_12 = await api.Trade.GetTradesHistoryAsync();
        var trade_13 = await api.Trade.GetTradesArchiveAsync(OkxInstrumentType.Futures);

        // AlgoTrading Methods (Signed)
        var algo_01 = await api.AlgoTrading.PlaceAlgoOrderAsync("BTC-USDT", OkxTradeMode.Isolated, OkxOrderSide.Sell, OkxAlgoOrderType.Conditional);
        var algo_02 = await api.AlgoTrading.CancelAlgoOrderAsync([]);
        var algo_03 = await api.AlgoTrading.AmendAlgoOrderAsync("BTC-USDT");
        var algo_04 = await api.AlgoTrading.CancelAdvanceAlgoOrderAsync([]);
        var algo_05 = await api.AlgoTrading.GetAlgoOrderAsync(algoOrderId: 1_000_001);
        var algo_06 = await api.AlgoTrading.GetAlgoOrdersAsync(OkxAlgoOrderType.OCO);
        var algo_07 = await api.AlgoTrading.GetAlgoOrderHistoryAsync(OkxAlgoOrderType.Conditional);

        // GridTrading Methods (Signed)
        var grid_01 = await api.GridTrading.PlaceAlgoOrderAsync(new OkxGridPlaceOrderRequest
        {
            Instrument = "BTC-USDT",
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
        var grid_02 = await api.GridTrading.PlaceAlgoOrderAsync(
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
        var grid_03 = await api.GridTrading.AmendAlgoOrderAsync(448965992920907776, "BTC-USDT-SWAP", 1200);
        var grid_04 = await api.GridTrading.StopAlgoOrderAsync(448965992920907776, "BTC-USDT", OkxGridAlgoOrderType.SpotGrid, OkxGridSpotAlgoStopType.SellBaseCurrency);
        var grid_05 = await api.GridTrading.CloseContractPositionAsync(448965992920907776, true);
        var grid_06 = await api.GridTrading.CancelCloseContractPositionAsync(448965992920907776, 570627699870375936);
        var grid_07 = await api.GridTrading.TriggerAlgoOrderAsync(448965992920907776);
        var grid_08 = await api.GridTrading.GetOpenAlgoOrdersAsync(OkxGridAlgoOrderType.SpotGrid);
        var grid_09 = await api.GridTrading.GetAlgoOrdersHistoryAsync(OkxGridAlgoOrderType.SpotGrid);
        var grid_10 = await api.GridTrading.GetAlgoOrderAsync(OkxGridAlgoOrderType.SpotGrid, 448965992920907776);
        var grid_11 = await api.GridTrading.GetAlgoSubOrdersAsync(OkxGridAlgoOrderType.SpotGrid, 448965992920907776, OkxGridAlgoSubOrderType.Live);
        var grid_12 = await api.GridTrading.GetAlgoPositionsAsync(OkxGridAlgoOrderType.ContractGrid, 448965992920907776);
        var grid_13 = await api.GridTrading.GetWithdrawIncomeAsync(448965992920907776);
        var grid_14 = await api.GridTrading.ComputeMarginBalanceAsync(448965992920907776, OkxMarginAddReduce.Add, 10.0m);
        var grid_15 = await api.GridTrading.AdjustMarginBalanceAsync(448965992920907776, OkxMarginAddReduce.Add, 10.0m);
        var grid_16 = await api.GridTrading.GetAiParameterAsync( OkxGridAlgoOrderType.SpotGrid, "BTC-USDT");
        var grid_17 = await api.GridTrading.ComputeMinimumInvestmentAsync("ETH-USDT",  OkxGridAlgoOrderType.SpotGrid, 5000, 3000, 50, OkxGridRunType.Arithmetic);
        var grid_18 = await api.GridTrading.RsiBackTestingAsync("BTC-USDT", OkxGridAlgoTimeFrame.ThreeMinutes, 30, 14);

        // TODO: RecurringBuy Methods (Signed)

        // CopyTrading Methods (Signed)
        var copy_01 = await api.CopyTrading.GetExistingLeadingPositionsAsync();
        var copy_02 = await api.CopyTrading.GetExistingLeadingPositionsHistoryAsync();
        var copy_03 = await api.CopyTrading.PlaceLeadingStopOrderAsync(leadingPositionId: 1_000_001);
        var copy_04 = await api.CopyTrading.CloseLeadingPositionAsync(leadingPositionId: 1_000_001);
        var copy_05 = await api.CopyTrading.GetLeadingInstrumentsAsync();
        var copy_06 = await api.CopyTrading.AmendLeadingInstrumentsAsync(["BTC-USDT", "ETH-USDT"]);
        var copy_07 = await api.CopyTrading.GetProfitSharingDetailsAsync();
        var copy_08 = await api.CopyTrading.GetTotalProfitSharingAsync();
        var copy_09 = await api.CopyTrading.GetUnrealizedProfitSharingDetailsAsync();

        // MarketData Methods (Unsigned)
        var market_01 = await api.Public.GetTickersAsync(OkxInstrumentType.Spot);
        var market_02 = await api.Public.GetTickerAsync("BTC-USDT");
        var market_04 = await api.Public.GetOrderBookAsync("BTC-USDT", 40);
        var market_05 = await api.Public.GetCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var market_06 = await api.Public.GetCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        var market_09 = await api.Public.GetTradesAsync("BTC-USDT");
        var market_10 = await api.Public.GetTradesHistoryAsync("BTC-USDT");
        var market_11 = await api.Public.Get24HourVolumeAsync();

        // BlockTrading Methods (Unsigned)
        var block_01 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Spot);
        var block_02 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Futures);
        var block_03 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Option);
        var block_04 = await api.BlockTrading.GetBlockTickersAsync(OkxInstrumentType.Swap);
        var block_05 = await api.BlockTrading.GetBlockTickerAsync("BTC-USDT");
        var block_06 = await api.BlockTrading.GetBlockTradesAsync("BTC-USDT");

        // TODO: SpreadTrading Methods (Signed)

        // PublicData Methods (Unsigned)
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
        var public_27 = await api.Public.UnitConvertAsync("BTC-USD-SWAP", price: 35000, size: 0.888m);
        var public_28 = await api.Public.GetIndexTickersAsync(instrumentId: "BTC-USDT");
        var public_29 = await api.Public.GetIndexCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_30 = await api.Public.GetMarkPriceCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        var public_31 = await api.Public.GetOracleAsync();
        var public_32 = await api.Public.GetExchangeRatesAsync();
        var public_33 = await api.Public.GetIndexComponentsAsync("BTC-USDT");

        // TradingStatistics Methods (Unsigned)
        var rubik_01 = await api.TradingStatistics.GetSupportCoinAsync();
        var rubik_02 = await api.TradingStatistics.GetTakerVolumeAsync("BTC", OkxInstrumentType.Spot);
        var rubik_03 = await api.TradingStatistics.GetMarginLendingRatioAsync("BTC", OkxPeriod.OneDay);
        var rubik_04 = await api.TradingStatistics.GetLongShortRatioAsync("BTC", OkxPeriod.OneDay);
        var rubik_05 = await api.TradingStatistics.GetContractSummaryAsync("BTC", OkxPeriod.OneDay);
        var rubik_06 = await api.TradingStatistics.GetOptionsSummaryAsync("BTC", OkxPeriod.OneDay);
        var rubik_07 = await api.TradingStatistics.GetPutCallRatioAsync("BTC", OkxPeriod.OneDay);
        var rubik_08 = await api.TradingStatistics.GetInterestVolumeExpiryAsync("BTC", OkxPeriod.OneDay);
        var rubik_09 = await api.TradingStatistics.GetInterestVolumeStrikeAsync("BTC", "20210623", OkxPeriod.OneDay);
        var rubik_10 = await api.TradingStatistics.GetTakerFlowAsync("BTC", OkxPeriod.OneDay);

        // FundingAccount Methods (Signed)
        var funding_01 = await api.Funding.GetCurrenciesAsync();
        var funding_02 = await api.Funding.GetBalancesAsync();
        var funding_03 = await api.Funding.FundTransferAsync("BTC", 0.5m, OkxTransferType.TransferWithinAccount, OkxAccount.Funding, OkxAccount.Trading);
        var funding_04 = await api.Funding.GetFundingBillDetailsAsync("BTC");
        var funding_05 = await api.Funding.GetLightningDepositsAsync("BTC", 0.001m);
        var funding_06 = await api.Funding.GetDepositAddressAsync("BTC");
        var funding_07 = await api.Funding.GetDepositAddressAsync("USDT");
        var funding_08 = await api.Funding.GetDepositHistoryAsync("USDT");
        var funding_09 = await api.Funding.WithdrawAsync("USDT", 100.0m, OkxWithdrawalDestination.DigitalCurrencyAddress, "toAddress", 1.0m, "USDT-TRC20");
        var funding_10 = await api.Funding.GetLightningWithdrawalsAsync("BTC", "invoice", "password");
        var funding_11 = await api.Funding.CancelWithdrawalAsync(1_000_001);
        var funding_12 = await api.Funding.GetWithdrawalHistoryAsync("USDT");

        // SubAccount Methods (Signed)
        var subaccount_01 = await api.SubAccount.GetSubAccountsAsync();
        var subaccount_02 = await api.SubAccount.ResetSubAccountApiKeyAsync("subAccountName", "apiKey", "apiLabel", true, true, "");
        var subaccount_03 = await api.SubAccount.GetSubAccountTradingBalancesAsync("subAccountName");
        var subaccount_04 = await api.SubAccount.GetSubAccountFundingBalancesAsync("subAccountName");
        var subaccount_05 = await api.SubAccount.GetSubAccountBillsAsync();
        var subaccount_06 = await api.SubAccount.TransferBetweenSubAccountsAsync("BTC", 0.5m, OkxAccount.Funding, OkxAccount.Trading, "fromSubAccountName", "toSubAccountName");

        // TODO: FinancialProduct.Earn Methods (Signed)
        // TODO: FinancialProduct.Savings Methods (Signed)
        #endregion

        #region WebSocket Api Client Examples
        // OKX Socket Client
        var ws = new OkxSocketApiClient();
        ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

        // TradingAccount Updates (Private)
        await ws.Account.SubscribeToAccountUpdatesAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        });
        await ws.Account.SubscribeToPositionUpdatesAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");
        await ws.Account.SubscribeToBalanceAndPositionUpdatesAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        });

        // Trade Updates (Private)
        await ws.Trade.SubscribeToOrderUpdatesAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");

        // AlgoTrading Updates (Private)
        await ws.AlgoTrading.SubscribeToAlgoOrderUpdatesAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");
        await ws.AlgoTrading.SubscribeToAdvanceAlgoOrderUpdatesAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        }, OkxInstrumentType.Futures, "INSTRUMENT-FAMILY", "INSTRUMENT-ID");

        // MarketData Updates (Public)
        var sample_pairs = new List<string> { "BTC-USDT", "LTC-USDT", "ETH-USDT", "XRP-USDT", "BCH-USDT", "EOS-USDT", "OKB-USDT", "ETC-USDT", "TRX-USDT", "BSV-USDT", "DASH-USDT", "NEO-USDT", "QTUM-USDT", "XLM-USDT", "ADA-USDT", "AE-USDT", "BLOC-USDT", "EGT-USDT", "IOTA-USDT", "SC-USDT", "WXT-USDT", "ZEC-USDT", };
        var subs = new List<WebSocketUpdateSubscription>();
        foreach (var pair in sample_pairs)
        {
            var subscription = await ws.Public.SubscribeToTickersAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                    Console.WriteLine($"Ticker {data.Instrument} Ask:{data.AskPrice} Bid:{data.BidPrice}");
                }
            }, pair);
            subs.Add(subscription.Data);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToCandlesticksAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair, OkxPeriod.FiveMinutes);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToTradesAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToOrderBookAsync((data) =>
            {
                if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                {
                    // ... Your logic here
                }
            }, pair, OkxOrderBookType.OrderBook);
        }

        // Unsubscribe
        foreach (var sub in subs)
        {
            _ = ws.UnsubscribeAsync(sub);
        }

        // TODO: BlockTrading Updates (Private)
        // TODO: SpreadTrading Updates (Private)

        // PublicData Updates (Public)
        await ws.Public.SubscribeToInstrumentsAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
                Console.WriteLine($"Instrument {data.Instrument} BaseCurrency:{data.BaseCurrency}");
            }
        }, OkxInstrumentType.Spot);
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToOpenInterestsAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToFundingRatesAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToPriceLimitAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair);
        }
        await ws.Public.SubscribeToOptionSummaryAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        }, "USD");
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToEstimatedPriceAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, OkxInstrumentType.Option);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToMarkPriceAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToIndexTickersAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToMarkPriceCandlesticksAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair, OkxPeriod.FiveMinutes);
        }
        foreach (var pair in sample_pairs)
        {
            await ws.Public.SubscribeToIndexCandlesticksAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            }, pair, OkxPeriod.FiveMinutes);
        }

        // TODO: TradingStatistics Updates (Private)
        // TODO: FundingAccount Updates (Private)
        // TODO: SubAccount Updates (Private)
        // TODO: FinancialProduct.Earn (Private)
        // TODO: FinancialProduct.Savings (Private)
        
        // Status Updates (Public)
        await ws.Status.SubscribeToSystemStatusAsync((data) =>
        {
            if (data != null)
            {
                // ... Your logic here
            }
        });
        #endregion

    }
}