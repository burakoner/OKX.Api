using ApiSharp.Stream;
using OKX.Api;
using OKX.Api.Enums;
using OKX.Api.Models.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKX.Api.Examples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region Rest Api Client Examples
            var api = new OKXRestApiClient();
            api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

            /* Public Endpoints (Unsigned) */
            var public_01 = await api.MarketData.GetInstrumentsAsync(OkxInstrumentType.Spot);
            var public_02 = await api.MarketData.GetInstrumentsAsync(OkxInstrumentType.Margin);
            var public_03 = await api.MarketData.GetInstrumentsAsync(OkxInstrumentType.Swap);
            var public_04 = await api.MarketData.GetInstrumentsAsync(OkxInstrumentType.Futures);
            var public_05 = await api.MarketData.GetInstrumentsAsync(OkxInstrumentType.Option, "USD");
            var public_06 = await api.MarketData.GetDeliveryExerciseHistoryAsync(OkxInstrumentType.Futures, "BTC-USD");
            var public_07 = await api.MarketData.GetDeliveryExerciseHistoryAsync(OkxInstrumentType.Option, "BTC-USD");
            var public_08 = await api.MarketData.GetOpenInterestsAsync(OkxInstrumentType.Futures);
            var public_09 = await api.MarketData.GetOpenInterestsAsync(OkxInstrumentType.Option, "BTC-USD");
            var public_10 = await api.MarketData.GetOpenInterestsAsync(OkxInstrumentType.Swap, "BTC-USD");
            var public_11 = await api.MarketData.GetFundingRatesAsync("BTC-USD-SWAP");
            var public_12 = await api.MarketData.GetFundingRateHistoryAsync("BTC-USD-SWAP");
            var public_13 = await api.MarketData.GetLimitPriceAsync("BTC-USD-SWAP");
            var public_14 = await api.MarketData.GetOptionMarketDataAsync("BTC-USD");
            var public_15 = await api.MarketData.GetEstimatedPriceAsync("BTC-USD-211004-41000-C");
            var public_16 = await api.MarketData.GetDiscountInfoAsync();
            var public_17 = await api.MarketData.GetServerTimeAsync();
            var public_18 = await api.MarketData.GetLiquidationOrdersAsync(OkxInstrumentType.Futures, underlying: "BTC-USD", alias: OkxInstrumentAlias.Quarter, state: OkxLiquidationState.Unfilled);
            var public_19 = await api.MarketData.GetMarkPricesAsync(OkxInstrumentType.Futures);
            var public_20 = await api.MarketData.GetPositionTiersAsync(OkxInstrumentType.Futures, OkxMarginMode.Isolated, "BTC-USD");
            var public_21 = await api.MarketData.GetInterestRatesAsync();
            var public_22 = await api.MarketData.GetVIPInterestRatesAsync();
            var public_23 = await api.MarketData.GetUnderlyingAsync(OkxInstrumentType.Futures);
            var public_24 = await api.MarketData.GetUnderlyingAsync(OkxInstrumentType.Option);
            var public_25 = await api.MarketData.GetUnderlyingAsync(OkxInstrumentType.Swap);
            var public_26 = await api.MarketData.GetInsuranceFundAsync(OkxInstrumentType.Margin, currency: "BTC");
            var public_27 = await api.MarketData.UnitConvertAsync(OkxConvertType.CurrencyToContract, instrumentId: "BTC-USD-SWAP", price: 35000, size: 0.888m);

            /* Market Endpoints (Unsigned) */
            var market_01 = await api.MarketData.GetTickersAsync(OkxInstrumentType.Spot);
            var market_02 = await api.MarketData.GetTickerAsync("BTC-USDT");
            var market_03 = await api.MarketData.GetIndexTickersAsync(instrumentId: "BTC-USDT");
            var market_04 = await api.MarketData.GetOrderBookAsync("BTC-USDT", 40);
            var market_05 = await api.MarketData.GetCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
            var market_06 = await api.MarketData.GetCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
            var market_07 = await api.MarketData.GetIndexCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
            var market_08 = await api.MarketData.GetMarkPriceCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
            var market_09 = await api.MarketData.GetTradesAsync("BTC-USDT");
            var market_10 = await api.MarketData.GetTradesHistoryAsync("BTC-USDT");
            var market_11 = await api.MarketData.Get24HourVolumeAsync();
            var market_12 = await api.MarketData.GetOracleAsync();
            var market_13 = await api.MarketData.GetIndexComponentsAsync("BTC-USDT");
            var market_14 = await api.MarketData.GetBlockTickersAsync(OkxInstrumentType.Spot);
            var market_15 = await api.MarketData.GetBlockTickersAsync(OkxInstrumentType.Futures);
            var market_16 = await api.MarketData.GetBlockTickersAsync(OkxInstrumentType.Option);
            var market_17 = await api.MarketData.GetBlockTickersAsync(OkxInstrumentType.Swap);
            var market_18 = await api.MarketData.GetBlockTickerAsync("BTC-USDT");
            var market_19 = await api.MarketData.GetBlockTradesAsync("BTC-USDT");

            /* Rubik Endpoints (Unsigned) */
            var rubik_01 = await api.Rubik.GetRubikSupportCoinAsync();
            var rubik_02 = await api.Rubik.GetRubikTakerVolumeAsync("BTC", OkxInstrumentType.Spot);
            var rubik_03 = await api.Rubik.GetRubikMarginLendingRatioAsync("BTC", OkxPeriod.OneDay);
            var rubik_04 = await api.Rubik.GetRubikLongShortRatioAsync("BTC", OkxPeriod.OneDay);
            var rubik_05 = await api.Rubik.GetRubikContractSummaryAsync("BTC", OkxPeriod.OneDay);
            var rubik_06 = await api.Rubik.GetRubikOptionsSummaryAsync("BTC", OkxPeriod.OneDay);
            var rubik_07 = await api.Rubik.GetRubikPutCallRatioAsync("BTC", OkxPeriod.OneDay);
            var rubik_08 = await api.Rubik.GetRubikInterestVolumeExpiryAsync("BTC", OkxPeriod.OneDay);
            var rubik_09 = await api.Rubik.GetRubikInterestVolumeStrikeAsync("BTC", "20210623", OkxPeriod.OneDay);
            var rubik_10 = await api.Rubik.GetRubikTakerFlowAsync("BTC", OkxPeriod.OneDay);

            /* Account Endpoints (Signed) */
            var account_01 = await api.Account.GetAccountBalanceAsync();
            var account_02 = await api.Account.GetAccountPositionsAsync();
            var account_03 = await api.Account.GetAccountPositionsHistoryAsync();
            var account_04 = await api.Account.GetAccountPositionRiskAsync();
            var account_05 = await api.Account.GetBillHistoryAsync();
            var account_06 = await api.Account.GetBillArchiveAsync();
            var account_07 = await api.Account.GetAccountConfigurationAsync();
            var account_08 = await api.Account.SetAccountPositionModeAsync(OkxPositionMode.LongShortMode);
            var account_09 = await api.Account.GetAccountLeverageAsync("BTC-USD-211008", OkxMarginMode.Isolated);
            var account_10 = await api.Account.SetAccountLeverageAsync(30, null, "BTC-USD-211008", OkxMarginMode.Isolated, OkxPositionSide.Long);
            var account_11 = await api.Account.GetMaximumAmountAsync("BTC-USDT", OkxTradeMode.Isolated);
            var account_12 = await api.Account.GetMaximumAvailableAmountAsync("BTC-USDT", OkxTradeMode.Isolated);
            var account_13 = await api.Account.SetMarginAmountAsync("BTC-USDT", OkxPositionSide.Long, OkxMarginAddReduce.Add, 100.0m);
            var account_14 = await api.Account.GetMaximumLoanAmountAsync("BTC-USDT", OkxMarginMode.Cross);
            var account_15 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Spot, category: OkxFeeRateCategory.ClassA);
            var account_16 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Futures, category: OkxFeeRateCategory.ClassA);
            var account_17 = await api.Account.GetInterestAccruedAsync();
            var account_18 = await api.Account.GetInterestRateAsync();
            var account_19 = await api.Account.SetGreeksAsync(OkxGreeksType.GreeksInCoins);
            var account_20 = await api.Account.GetMaximumWithdrawalsAsync();

            /* SubAccount Endpoints (Signed) */
            var subaccount_01 = await api.SubAccount.GetSubAccountsAsync();
            var subaccount_02 = await api.SubAccount.ResetSubAccountApiKeyAsync("subAccountName", "apiKey", "apiLabel", true, true, "");
            var subaccount_03 = await api.SubAccount.GetSubAccountTradingBalancesAsync("subAccountName");
            var subaccount_04 = await api.SubAccount.GetSubAccountFundingBalancesAsync("subAccountName");
            var subaccount_05 = await api.SubAccount.GetSubAccountBillsAsync();
            var subaccount_06 = await api.SubAccount.TransferBetweenSubAccountsAsync("BTC", 0.5m, OkxAccount.Funding, OkxAccount.Trading, "fromSubAccountName", "toSubAccountName");

            /* Funding Endpoints (Signed) */
            var funding_01 = await api.Funding.GetCurrenciesAsync();
            var funding_02 = await api.Funding.GetFundingBalanceAsync();
            var funding_03 = await api.Funding.FundTransferAsync("BTC", 0.5m, OkxTransferType.TransferWithinAccount, OkxAccount.Funding, OkxAccount.Trading);
            var funding_04 = await api.Funding.GetFundingBillDetailsAsync("BTC");
            var funding_05 = await api.Funding.GetLightningDepositsAsync("BTC", 0.001m);
            var funding_06 = await api.Funding.GetDepositAddressAsync("BTC");
            var funding_07 = await api.Funding.GetDepositAddressAsync("USDT");
            var funding_08 = await api.Funding.GetDepositHistoryAsync("USDT");
            var funding_09 = await api.Funding.WithdrawAsync("USDT", 100.0m, OkxWithdrawalDestination.DigitalCurrencyAddress, "toAddress", "password", 1.0m, "USDT-TRC20");
            var funding_10 = await api.Funding.GetLightningWithdrawalsAsync("BTC", "invoice", "password");
            var funding_11 = await api.Funding.GetWithdrawalHistoryAsync("USDT");
            var funding_12 = await api.Funding.GetSavingBalancesAsync();
            var funding_13 = await api.Funding.SavingPurchaseRedemptionAsync("USDT", 10.0m, OkxSavingActionSide.Purchase);

            /* Trade Endpoints (Signed) */
            var trade_01 = await api.Trade.PlaceOrderAsync("BTC-USDT", OkxTradeMode.Cash, OkxOrderSide.Buy, OkxPositionSide.Long, OkxOrderType.MarketOrder, 0.1m);
            var trade_02 = await api.Trade.PlaceMultipleOrdersAsync(new List<OkxOrderPlaceRequest>());
            var trade_03 = await api.Trade.CancelOrderAsync("BTC-USDT");
            var trade_04 = await api.Trade.CancelMultipleOrdersAsync(new List<OkxOrderCancelRequest>());
            var trade_05 = await api.Trade.AmendOrderAsync("BTC-USDT");
            var trade_06 = await api.Trade.AmendMultipleOrdersAsync(new List<OkxOrderAmendRequest>());
            var trade_07 = await api.Trade.ClosePositionAsync("BTC-USDT", OkxMarginMode.Isolated);
            var trade_08 = await api.Trade.GetOrderDetailsAsync("BTC-USDT");
            var trade_09 = await api.Trade.GetOrderListAsync();
            var trade_10 = await api.Trade.GetOrderHistoryAsync(OkxInstrumentType.Swap);
            var trade_11 = await api.Trade.GetOrderArchiveAsync(OkxInstrumentType.Futures);
            var trade_12 = await api.Trade.GetTransactionHistoryAsync();
            var trade_13 = await api.Trade.GetTransactionArchiveAsync(OkxInstrumentType.Futures);
            var trade_14 = await api.Trade.PlaceAlgoOrderAsync("BTC-USDT", OkxTradeMode.Isolated, OkxOrderSide.Sell, OkxAlgoOrderType.Conditional, 0.1m);
            var trade_15 = await api.Trade.CancelAlgoOrderAsync(new List<OkxAlgoOrderRequest>());
            var trade_16 = await api.Trade.CancelAdvanceAlgoOrderAsync(new List<OkxAlgoOrderRequest>());
            var trade_17 = await api.Trade.GetAlgoOrderListAsync(OkxAlgoOrderType.OCO);
            var trade_18 = await api.Trade.GetAlgoOrderHistoryAsync(OkxAlgoOrderType.Conditional);
            #endregion

            #region WebSocket Api Client Examples
            /* OKX Socket Client */
            var ws = new OKXStreamClient();
            ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

            /* Sample Pairs */
            var sample_pairs = new List<string> { "BTC-USDT", "LTC-USDT", "ETH-USDT", "XRP-USDT", "BCH-USDT", "EOS-USDT", "OKB-USDT", "ETC-USDT", "TRX-USDT", "BSV-USDT", "DASH-USDT", "NEO-USDT", "QTUM-USDT", "XLM-USDT", "ADA-USDT", "AE-USDT", "BLOC-USDT", "EGT-USDT", "IOTA-USDT", "SC-USDT", "WXT-USDT", "ZEC-USDT", };

            /* WS Subscriptions */
            var subs = new List<UpdateSubscription>();

            /* Instruments (Public) */
            await ws.SubscribeToInstrumentsAsync(OkxInstrumentType.Spot, (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                    Console.WriteLine($"Instrument {data.Instrument} BaseCurrency:{data.BaseCurrency} Category:{data.Category}");
                }
            });

            /* Tickers (Public) */
            foreach (var pair in sample_pairs)
            {
                var subscription = await ws.SubscribeToTickersAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                        Console.WriteLine($"Ticker {data.Instrument} Ask:{data.AskPrice} Bid:{data.BidPrice}");
                    }
                });
                subs.Add(subscription.Data);
            }

            /* Unsubscribe */
            foreach (var sub in subs)
            {
                _ = ws.UnsubscribeAsync(sub);
            }

            /* Interests (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToInterestsAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Candlesticks (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToCandlesticksAsync(pair, OkxPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Trades (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToTradesAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Estimated Price (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToTradesAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Mark Price (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToMarkPriceAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Mark Price Candlesticks (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToMarkPriceCandlesticksAsync(pair, OkxPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Limit Price (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToPriceLimitAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Order Book (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToOrderBookAsync(pair, OkxOrderBookType.OrderBook, (data) =>
                {
                    if (data != null && data.Asks != null && data.Asks.Count() > 0 && data.Bids != null && data.Bids.Count() > 0)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Option Summary (Public) */
            await ws.SubscribeToOptionSummaryAsync("USD", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });

            /* Funding Rates (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToFundingRatesAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Index Candlesticks (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToIndexCandlesticksAsync(pair, OkxPeriod.FiveMinutes, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* Index Tickers (Public) */
            foreach (var pair in sample_pairs)
            {
                await ws.SubscribeToIndexTickersAsync(pair, (data) =>
                {
                    if (data != null)
                    {
                        // ... Your logic here
                    }
                });
            }

            /* System Status (Public) */
            await ws.SubscribeToSystemStatusAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });

            /* Account Updates (Private) */
            await ws.SubscribeToAccountUpdatesAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });

            /* Position Updates (Private) */
            await ws.SubscribeToPositionUpdatesAsync(OkxInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });

            /* Balance And Position Updates (Private) */
            await ws.SubscribeToBalanceAndPositionUpdatesAsync((data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });

            /* Order Updates (Private) */
            await ws.SubscribeToOrderUpdatesAsync(OkxInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });

            /* Algo Order Updates (Private) */
            await ws.SubscribeToAlgoOrderUpdatesAsync(OkxInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });

            /* Advance Algo Order Updates (Private) */
            await ws.SubscribeToAdvanceAlgoOrderUpdatesAsync(OkxInstrumentType.Futures, "INSTRUMENT", "UNDERLYING", (data) =>
            {
                if (data != null)
                {
                    // ... Your logic here
                }
            });
            #endregion

        }
    }
}