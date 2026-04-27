namespace OKX.Api.Examples;

internal static class Program
{
    private const string ExampleApiKey = "YOUR-OKX-API-KEY";
    private const string ExampleApiSecret = "YOUR-OKX-API-SECRET";
    private const string ExampleApiPassphrase = "YOUR-OKX-API-PASSPHRASE";

    private const string ExampleInstrumentId = "BTC-USDT";
    private const string ExampleSwapInstrumentId = "BTC-USDT-SWAP";
    private const string ExampleFutureInstrumentId = "BTC-USD-240628";
    private const string ExampleInstrumentFamily = "BTC-USDT";
    private const string ExampleFutureFamily = "BTC-USD";
    private const string ExampleOptionFamily = "BTC-USD";
    private const string ExampleSpreadId = "BTC-USDT_BTC-USD-SWAP";
    private const string ExampleLeadTraderCode = "REPLACE-WITH-LEAD-TRADER-CODE";
    private const string ExampleSubAccountName = "REPLACE-WITH-SUB-ACCOUNT";
    private const string ExampleProductId = "REPLACE-WITH-PRODUCT-ID";
    private const string ExampleSignalDescription = "Signal bot example channel";
    private const string ExampleSignalName = "OKX.Api Signal Example";
    private const string ExampleTimeZone = "UTC";
    private const string ExampleCurrency = "BTC";
    private const string ExampleQuoteCurrency = "USDT";

    private const long ExampleOrderId = 123456789L;
    private const long ExampleAlgoOrderId = 123456789L;
    private const long ExampleSubOrderId = 123456789L;
    private const long ExampleSignalChannelId = 123456789L;
    private const long ExampleInviteeUid = 1_000_000L;

    private static Task Main()
    {
        Console.WriteLine("OKX.Api.Examples/Program.cs is a reference catalog.");
        Console.WriteLine("Open this file and copy the section that matches the client or endpoint family you want to use.");
        return Task.CompletedTask;
    }

    #region Client Setup
    private static void RestClientSetupReference()
    {
        // Public-only REST client. No API key is required.
        var publicApi = new OkxRestApiClient(new OkxRestApiOptions
        {
            SignPublicRequests = false,
            AutoTimestamp = true,
            AutoTimestampInterval = TimeSpan.FromHours(1)
        });

        // Authenticated REST client. Use this for Trading Account, Trade, Funding, Financial, etc.
        var privateApi = CreatePrivateRestClient();

        // Authenticated demo client.
        var demoApi = CreatePrivateRestClient(demoTrading: true);

        // Authenticated client that is also allowed to sign public requests when needed.
        var signedPublicApi = CreatePrivateRestClient(signPublicRequests: true);

        _ = publicApi;
        _ = privateApi;
        _ = demoApi;
        _ = signedPublicApi;
    }

    private static void WebSocketClientSetupReference()
    {
        // Public WebSocket client. No API key is required for public channels.
        var publicWs = new OkxWebSocketApiClient(new OkxWebSocketApiOptions());

        // Authenticated WebSocket client for private channels and trading operations.
        var privateWs = CreatePrivateWebSocketClient();

        // Demo WebSocket client.
        var demoWs = CreatePrivateWebSocketClient(demoTrading: true);

        _ = publicWs;
        _ = privateWs;
        _ = demoWs;
    }
    #endregion

    #region REST - Public / Market Data
    private static async Task PublicRestReferenceAsync()
    {
        var api = CreatePublicRestClient();

        // Instruments
        _ = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Spot);
        _ = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Margin);
        _ = await api.Public.GetInstrumentsAsync(new OkxPublicInstrumentQueryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentFamily = ExampleInstrumentFamily
        });

        // Core market data
        _ = await api.Public.GetServerTimeAsync();
        _ = await api.Public.GetTickersAsync(OkxInstrumentType.Spot);
        _ = await api.Public.GetTickerAsync(ExampleInstrumentId);
        _ = await api.Public.GetOrderBookAsync(ExampleInstrumentId, 40);
        _ = await api.Public.GetOrderBookFullAsync(ExampleInstrumentId, 400);
        _ = await api.Public.GetTradesAsync(ExampleInstrumentId);
        _ = await api.Public.GetTradeHistoryAsync(ExampleInstrumentId);

        // Candlesticks
        _ = await api.Public.GetCandlesticksAsync(ExampleInstrumentId, OkxPeriod.OneHour);
        _ = await api.Public.GetCandlestickHistoryAsync(ExampleInstrumentId, OkxPeriod.OneHour);
        _ = await api.Public.GetCandlesticksAsync(new OkxPublicCandlestickRequest
        {
            InstrumentId = ExampleInstrumentId,
            Period = "1H",
            Limit = 20
        });

        // Derivatives / funding / mark price
        _ = await api.Public.GetOpenInterestsAsync(OkxInstrumentType.Swap, ExampleFutureFamily);
        _ = await api.Public.GetFundingRatesAsync(ExampleSwapInstrumentId);
        _ = await api.Public.GetFundingRateHistoryAsync(new OkxPublicFundingRateHistoryRequest
        {
            InstrumentId = ExampleSwapInstrumentId,
            Limit = 20
        });
        _ = await api.Public.GetLimitPriceAsync(ExampleSwapInstrumentId);
        _ = await api.Public.GetMarkPricesAsync(OkxInstrumentType.Swap, ExampleFutureFamily);
        _ = await api.Public.GetPremiumHistoryAsync(ExampleSwapInstrumentId);

        // Derivatives / options / index data
        _ = await api.Public.GetDeliveryExerciseHistoryAsync(OkxInstrumentType.Futures, ExampleFutureFamily);
        _ = await api.Public.GetOptionMarketDataAsync(ExampleOptionFamily);
        _ = await api.Public.GetEstimatedPriceAsync("BTC-USD-240628-50000-C");
        _ = await api.Public.GetOptionTickBandsAsync();
        _ = await api.Public.GetUnderlyingAsync(OkxInstrumentType.Option);
        _ = await api.Public.GetIndexTickersAsync("BTC-USDT");
        _ = await api.Public.GetIndexCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
        _ = await api.Public.GetIndexCandlesticksHistoryAsync("BTC-USDT", OkxPeriod.OneHour);
        _ = await api.Public.GetIndexComponentsAsync("BTC-USDT");
        _ = await api.Public.GetMarkPriceCandlesticksAsync(ExampleSwapInstrumentId, OkxPeriod.OneHour);
        _ = await api.Public.GetMarkPriceCandlesticksHistoryAsync(ExampleSwapInstrumentId, OkxPeriod.OneHour);

        // Public data / informational endpoints
        _ = await api.Public.GetDiscountInfoAsync();
        _ = await api.Public.GetInterestRateLoanQuotaAsync();
        _ = await api.Public.GetInsuranceFundsAsync(OkxInstrumentType.Margin, currency: ExampleCurrency);
        _ = await api.Public.GetExchangeRateAsync();
        _ = await api.Public.GetEconomicCalendarDataAsync("united_states");
        _ = await api.Public.GetEconomicCalendarDataAsync(new OkxPublicEconomicCalendarRequest
        {
            Region = "united_states",
            Limit = 20
        });
        _ = await api.Public.GetAnnouncementTypesAsync();
        _ = await api.Public.GetAnnouncementsAsync();
        _ = await api.Public.GetSystemUpgradeStatusAsync();

        // Event contracts
        _ = await api.Public.GetEventContractSeriesAsync();
    }
    #endregion

    #region REST - Trading Account
    private static async Task AccountRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        // Read-only account endpoints
        _ = await api.Account.GetInstrumentsAsync(OkxInstrumentType.Spot);
        _ = await api.Account.GetBalancesAsync();
        _ = await api.Account.GetPositionsAsync();
        _ = await api.Account.GetPositionsHistoryAsync(new OkxAccountPositionsHistoryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentId = ExampleSwapInstrumentId,
            Limit = 20
        });
        _ = await api.Account.GetPositionRiskAsync();
        _ = await api.Account.GetConfigurationAsync();
        _ = await api.Account.GetGreeksAsync();
        _ = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Spot);
        _ = await api.Account.GetInterestAccruedAsync();
        _ = await api.Account.GetInterestRateAsync();
        _ = await api.Account.GetMaximumOrderQuantityAsync(ExampleInstrumentId, OkxTradeMode.Cash);
        _ = await api.Account.GetMaximumAvailableAmountAsync(ExampleInstrumentId, OkxTradeMode.Cash);
        _ = await api.Account.GetMaximumLoanAmountAsync(new OkxAccountMaximumLoanAmountRequest
        {
            MarginMode = OkxAccountMarginMode.Cross,
            InstrumentId = ExampleInstrumentId
        });
        _ = await api.Account.GetMaximumWithdrawalsAsync();
        _ = await api.Account.GetRiskStateAsync();
        _ = await api.Account.GetPositionTiersAsync(OkxInstrumentType.Futures);
        _ = await api.Account.GetBillHistoryAsync(new OkxAccountBillQueryRequest
        {
            Currency = ExampleCurrency,
            Limit = 20
        });
        _ = await api.Account.GetBillArchiveAsync(new OkxAccountBillQueryRequest
        {
            Currency = ExampleCurrency,
            Limit = 20
        });

        // Configuration / account-changing endpoints
        _ = await api.Account.SetPositionModeAsync(OkxTradePositionMode.LongShortMode);
        _ = await api.Account.SetLeverageAsync(new OkxAccountSetLeverageRequest
        {
            Leverage = 5,
            InstrumentId = ExampleSwapInstrumentId,
            MarginMode = OkxAccountMarginMode.Isolated,
            PositionSide = OkxTradePositionSide.Long
        });
        _ = await api.Account.SetGreeksAsync(OkxAccountGreeksType.GreeksInCoins);
        _ = await api.Account.SetAutoLoanAsync(true);
        _ = await api.Account.SetLevelAsync(OkxAccountMode.SpotAndFuturesMode);
        _ = await api.Account.SetTradingConfigAsync(OkxAccountStrategyType.DeltaNeutral);
        _ = await api.Account.PrecheckSetDeltaNeutralAsync(OkxAccountStrategyType.DeltaNeutral);
    }
    #endregion

    #region REST - Trade
    private static async Task TradeRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        // Order placement
        _ = await api.Trade.PlaceOrderAsync(
            ExampleInstrumentId,
            OkxTradeMode.Cash,
            OkxTradeOrderSide.Buy,
            OkxTradePositionSide.Net,
            OkxTradeOrderType.LimitOrder,
            0.001m,
            1000m);

        _ = await api.Trade.PlaceOrderAsync(new OkxTradeOrderPlaceRequest
        {
            InstrumentId = ExampleInstrumentId,
            TradeMode = OkxTradeMode.Cash,
            OrderSide = OkxTradeOrderSide.Buy,
            PositionSide = OkxTradePositionSide.Net,
            OrderType = OkxTradeOrderType.MarketOrder,
            Size = 0.001m
        });

        // Order maintenance
        _ = await api.Trade.CancelOrderAsync(ExampleInstrumentId, orderId: ExampleOrderId);
        _ = await api.Trade.AmendOrderAsync(ExampleInstrumentId, orderId: ExampleOrderId, newQuantity: 0.002m);
        _ = await api.Trade.ClosePositionAsync(new OkxTradeClosePositionRequest
        {
            InstrumentId = ExampleSwapInstrumentId,
            MarginMode = OkxAccountMarginMode.Isolated
        });

        // Queries
        _ = await api.Trade.GetOrderAsync(ExampleInstrumentId, orderId: ExampleOrderId);
        _ = await api.Trade.GetOpenOrdersAsync(new OkxTradeOpenOrdersRequest
        {
            InstrumentType = OkxInstrumentType.Spot,
            InstrumentId = ExampleInstrumentId,
            Limit = 20
        });
        _ = await api.Trade.GetOrderHistoryAsync(new OkxTradeOrderQueryRequest
        {
            InstrumentType = OkxInstrumentType.Spot,
            InstrumentId = ExampleInstrumentId,
            Limit = 20
        });
        _ = await api.Trade.GetOrderArchiveAsync(new OkxTradeOrderQueryRequest
        {
            InstrumentType = OkxInstrumentType.Futures,
            InstrumentFamily = ExampleFutureFamily,
            Limit = 20
        });
        _ = await api.Trade.GetTradesAsync(new OkxTradeTransactionQueryRequest
        {
            InstrumentType = OkxInstrumentType.Spot,
            InstrumentId = ExampleInstrumentId,
            Limit = 20
        });
        _ = await api.Trade.GetTradesHistoryAsync(new OkxTradeTransactionQueryRequest
        {
            InstrumentType = OkxInstrumentType.Spot,
            InstrumentId = ExampleInstrumentId,
            Limit = 20
        });
        _ = await api.Trade.GetAccountRateLimitAsync();

        // Precheck / account-level actions
        _ = await api.Trade.OrderPrecheckAsync(new OkxTradeOrderPrecheckRequest
        {
            InstrumentId = ExampleInstrumentId,
            TradeMode = OkxTradeMode.Cash,
            OrderSide = OkxTradeOrderSide.Buy,
            OrderType = OkxTradeOrderType.LimitOrder,
            Size = 0.001m,
            Price = 1000m
        });
        _ = await api.Trade.CancelAllAfterAsync(30);

        // Convert / repay helpers
        _ = await api.Trade.GetEasyConvertCurrenciesAsync();
        _ = await api.Trade.GetEasyConvertHistoryAsync();
        _ = await api.Trade.GetOneClickRepayCurrenciesAsync(OkxTradeDebtType.Isolated);
        _ = await api.Trade.GetOneClickRepayHistoryAsync();
    }
    #endregion

    #region REST - Algo Trading
    private static async Task AlgoRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.Algo.PlaceOrderAsync(ExampleInstrumentId, OkxTradeMode.Cash, OkxTradeOrderSide.Buy, OkxAlgoOrderType.Conditional);
        _ = await api.Algo.CancelOrderAsync(ExampleAlgoOrderId, ExampleInstrumentId);
        _ = await api.Algo.AmendOrderAsync(ExampleInstrumentId, algoOrderId: ExampleAlgoOrderId);
        _ = await api.Algo.GetOrderAsync(algoOrderId: ExampleAlgoOrderId);
        _ = await api.Algo.GetOpenOrdersAsync(OkxAlgoOrderType.Conditional);
        _ = await api.Algo.GetOrderHistoryAsync(OkxAlgoOrderType.Conditional);
    }
    #endregion

    #region REST - Grid Trading
    private static async Task GridRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.Grid.PlaceOrderAsync(new OkxGridPlaceOrderRequest
        {
            InstrumentId = ExampleInstrumentId,
            AlgoOrderType = OkxGridAlgoOrderType.SpotGrid,
            MaximumPrice = 50000m,
            MinimumPrice = 40000m,
            GridNumber = 10,
            GridRunType = OkxGridRunType.Arithmetic,
            QuoteSize = 100m
        });

        _ = await api.Grid.AmendOrderAsync(new OkxGridOrderAmendRequest
        {
            AlgoOrderId = ExampleAlgoOrderId,
            InstrumentId = ExampleInstrumentId
        });

        _ = await api.Grid.StopOrderAsync(ExampleAlgoOrderId, ExampleInstrumentId, OkxGridAlgoOrderType.SpotGrid, OkxGridSpotAlgoStopType.SellBaseCurrency);
        _ = await api.Grid.GetOpenOrdersAsync(OkxGridAlgoOrderType.SpotGrid);
        _ = await api.Grid.GetOrdersHistoryAsync(OkxGridAlgoOrderType.SpotGrid);
        _ = await api.Grid.GetOrderAsync(OkxGridAlgoOrderType.SpotGrid, ExampleAlgoOrderId);
        _ = await api.Grid.GetSubOrdersAsync(OkxGridAlgoOrderType.SpotGrid, ExampleAlgoOrderId, OkxGridAlgoSubOrderType.Live);
        _ = await api.Grid.GetAIParameterAsync(OkxGridAlgoOrderType.SpotGrid, ExampleInstrumentId);
    }
    #endregion

    #region REST - DCA Trading
    private static async Task DcaRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.DCA.PlaceOrderAsync(new OkxDcaCreateOrderRequest
        {
            InstrumentId = ExampleInstrumentId,
            AlgoOrderType = OkxDcaAlgoOrderType.SpotDca,
            InitialOrderAmount = 25m,
            MaximumSafetyOrders = 3,
            TakeProfitTarget = 0.05m,
            TriggerParameters =
            [
                new OkxDcaTriggerParametersRequest
                {
                    TriggerAction = OkxDcaTriggerAction.Start,
                    TriggerStrategy = OkxDcaTriggerStrategy.Instant
                }
            ]
        });

        _ = await api.DCA.AmendOrderAsync(new OkxDcaAmendOrderRequest
        {
            AlgoOrderId = ExampleAlgoOrderId,
            TakeProfitTarget = 0.06m
        });

        _ = await api.DCA.StopOrderAsync(ExampleAlgoOrderId, OkxDcaAlgoOrderType.SpotDca, OkxDcaStopType.CloseOrSell);
        _ = await api.DCA.GetOrderAsync(ExampleAlgoOrderId, OkxDcaAlgoOrderType.SpotDca);
        _ = await api.DCA.GetOpenOrdersAsync(OkxDcaAlgoOrderType.SpotDca);
        _ = await api.DCA.GetOrderHistoryAsync(OkxDcaAlgoOrderType.SpotDca);
        _ = await api.DCA.GetSubOrdersAsync(ExampleAlgoOrderId, OkxDcaAlgoOrderType.SpotDca);
        _ = await api.DCA.GetPositionDetailsAsync(ExampleAlgoOrderId, OkxDcaAlgoOrderType.SpotDca);
        _ = await api.DCA.GetCyclesAsync(ExampleAlgoOrderId, OkxDcaAlgoOrderType.SpotDca);
    }
    #endregion

    #region REST - Signal Bot
    private static async Task SignalBotRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.SignalBot.CreateChannelAsync(ExampleSignalName, ExampleSignalDescription);
        _ = await api.SignalBot.GetChannelsAsync(OkxSignalBotSourceType.FreeSignal);

        _ = await api.SignalBot.CreateSignalBotAsync(
            signalChannelId: ExampleSignalChannelId,
            instrumentIds:
            [
                ExampleSwapInstrumentId
            ],
            leverage: 5,
            amount: 100m,
            orderType: OkxSignalBotOrderType.TradingView,
            entryParamaters: new OkxSignalBotEntryParamaters
            {
                AllowMultipleEntry = true,
                EntryType = OkxSignalBotEntryType.TradingViewSignal
            },
            exitParamaters: new OkxSignalBotExitParamaters
            {
                TakeProfitStopLossTriggerPrice = OkxSignalBotTriggerPrice.Price
            });

        _ = await api.SignalBot.CancelSignalBotsAsync([ExampleAlgoOrderId]);
    }
    #endregion

    #region REST - Recurring Buy
    private static async Task RecurringBuyRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.RecurringBuy.PlaceOrderAsync(
            "Monthly BTC accumulation",
            [],
            OkxRecurringBuyPeriod.Monthly,
            recurringDay: 1,
            recurringHour: null,
            recurringTime: 1,
            timeZone: ExampleTimeZone,
            amount: 100m,
            investmentCurrency: ExampleQuoteCurrency,
            tradeMode: OkxTradeMode.Cash);

        _ = await api.RecurringBuy.AmendOrderAsync(ExampleAlgoOrderId, "Monthly BTC accumulation");
        _ = await api.RecurringBuy.StopOrderAsync(ExampleAlgoOrderId);
        _ = await api.RecurringBuy.PauseAsync(ExampleAlgoOrderId);
        _ = await api.RecurringBuy.RestartAsync(ExampleAlgoOrderId);
        _ = await api.RecurringBuy.GetOpenOrdersAsync();
        _ = await api.RecurringBuy.GetOrderHistoryAsync();
        _ = await api.RecurringBuy.GetOrderAsync(ExampleAlgoOrderId);
        _ = await api.RecurringBuy.GetSubOrdersAsync(ExampleAlgoOrderId);
    }
    #endregion

    #region REST - Copy Trading
    private static async Task CopyTradingRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        // Lead trader endpoints
        _ = await api.CopyTrading.GetLeadingPositionsAsync();
        _ = await api.CopyTrading.GetLeadingPositionsHistoryAsync();
        _ = await api.CopyTrading.GetLeadingInstrumentsAsync();
        _ = await api.CopyTrading.SetLeadingInstrumentsAsync([ExampleInstrumentId]);
        _ = await api.CopyTrading.GetProfitSharingDetailsAsync();
        _ = await api.CopyTrading.GetTotalProfitSharingAsync();
        _ = await api.CopyTrading.GetUnrealizedProfitSharingDetailsAsync();
        _ = await api.CopyTrading.GetTotalUnrealizedProfitSharingAsync();
        _ = await api.CopyTrading.AmendProfitSharingRatioAsync(0.1m);
        _ = await api.CopyTrading.GetAccountConfigurationAsync();

        // Copy trader endpoints
        _ = await api.CopyTrading.FirstCopySettingsAsync(
            ExampleLeadTraderCode,
            OkxCopyTradingMarginMode.Copy,
            OkxCopyTradingInstrumentIdType.Copy,
            100m,
            OkxCopyTradingPositionCloseType.CopyClose);

        _ = await api.CopyTrading.AmendCopySettingsAsync(
            ExampleLeadTraderCode,
            OkxCopyTradingMarginMode.Copy,
            OkxCopyTradingInstrumentIdType.Copy,
            100m,
            OkxCopyTradingPositionCloseType.CopyClose);

        _ = await api.CopyTrading.StopCopyingAsync(ExampleLeadTraderCode, OkxCopyTradingPositionCloseType.CopyClose, OkxInstrumentType.Swap);
        _ = await api.CopyTrading.GetCopySettingsAsync(ExampleLeadTraderCode);

        // Public lead trader discovery
        _ = await api.CopyTrading.GetMyLeadTradersAsync();
        _ = await api.CopyTrading.GetPublicConfigurationAsync();
        _ = await api.CopyTrading.GetLeadTradersRanksAsync();
        _ = await api.CopyTrading.GetLeadTraderWeeklyPnlAsync(ExampleLeadTraderCode);
        _ = await api.CopyTrading.GetLeadTraderDailyPnlAsync(ExampleLeadTraderCode, string.Empty);
        _ = await api.CopyTrading.GetLeadTraderStatsAsync(ExampleLeadTraderCode, string.Empty);
        _ = await api.CopyTrading.GetLeadTraderCurrencyPreferencesAsync(ExampleLeadTraderCode);
        _ = await api.CopyTrading.GetLeadTraderCurrentPositionsAsync(ExampleLeadTraderCode);
        _ = await api.CopyTrading.GetLeadTraderPositionHistoryAsync(ExampleLeadTraderCode);
        _ = await api.CopyTrading.GetCopyTradersAsync(ExampleLeadTraderCode);
    }
    #endregion

    #region REST - Block Trading
    private static async Task BlockRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.Block.GetCounterpartiesAsync();
        _ = await api.Block.GetQuoteProductsAsync();
        _ = await api.Block.GetRfqsAsync();
        _ = await api.Block.GetQuotesAsync();
        _ = await api.Block.GetTradesAsync();
        _ = await api.Block.GetTickersAsync(OkxInstrumentType.Futures);
        _ = await api.Block.GetTickerAsync(ExampleFutureInstrumentId);
        _ = await api.Block.GetPublicExecutedTradesAsync();
        _ = await api.Block.GetPublicRecentTradesAsync(ExampleFutureInstrumentId);

        _ = await api.Block.CreateRfqAsync([], []);
        _ = await api.Block.CancelAllRfqsAsync();
        _ = await api.Block.SetQuoteProductsAsync([]);
        _ = await api.Block.CreateQuoteAsync(string.Empty, OkxTradeOrderSide.Buy, []);
        _ = await api.Block.CancelAllQuotesAsync();
        _ = await api.Block.CancelAllQuotesAfterAsync(30);
    }
    #endregion

    #region REST - Spread Trading
    private static async Task SpreadRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.Spread.PlaceOrderAsync(ExampleSpreadId, OkxTradeOrderSide.Buy, OkxSpreadOrderType.LimitOrder, 1m, 10m);
        _ = await api.Spread.CancelOrderAsync(orderId: ExampleOrderId);
        _ = await api.Spread.AmendOrderAsync(new OkxSpreadRestOrderAmendRequest
        {
            OrderId = ExampleOrderId,
            NewPrice = 11m
        });

        _ = await api.Spread.GetOrderAsync(orderId: ExampleOrderId);
        _ = await api.Spread.GetOpenOrdersAsync(new OkxSpreadOrderQueryRequest
        {
            SpreadId = ExampleSpreadId,
            Limit = 20
        });
        _ = await api.Spread.GetOrderHistoryAsync(new OkxSpreadOrderQueryRequest
        {
            SpreadId = ExampleSpreadId,
            Limit = 20
        });
        _ = await api.Spread.GetOrderArchiveAsync(new OkxSpreadOrderQueryRequest
        {
            SpreadId = ExampleSpreadId,
            Limit = 20
        });
        _ = await api.Spread.GetTradesAsync(new OkxSpreadTradeQueryRequest
        {
            SpreadId = ExampleSpreadId,
            Limit = 20
        });
        _ = await api.Spread.GetSpreadsAsync(new OkxSpreadInstrumentQueryRequest
        {
            BaseCurrency = ExampleCurrency
        });
        _ = await api.Spread.GetOrderBookAsync(ExampleSpreadId);
        _ = await api.Spread.GetTickerAsync(ExampleSpreadId);
        _ = await api.Spread.GetTradesAsync(ExampleSpreadId, default);
        _ = await api.Spread.GetCandlesticksAsync(new OkxSpreadCandlestickRequest
        {
            SpreadId = ExampleSpreadId,
            Period = "1H",
            Limit = 20
        });
        _ = await api.Spread.GetCandlesticksHistoryAsync(new OkxSpreadCandlestickRequest
        {
            SpreadId = ExampleSpreadId,
            Period = "1H",
            Limit = 20
        });
        _ = await api.Spread.CancelAllAfterAsync(30);
    }
    #endregion

    #region REST - Trading Statistics
    private static async Task TradingStatisticsRestReferenceAsync()
    {
        var api = CreatePublicRestClient();

        _ = await api.TradingStatistics.GetSupportCoinAsync();
        _ = await api.TradingStatistics.GetContractOpenInterestHistoryAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = ExampleSwapInstrumentId,
            Period = "1D",
            Limit = 20
        });
        _ = await api.TradingStatistics.GetTakerVolumeAsync(new OkxRubikTakerVolumeRequest
        {
            Currency = ExampleCurrency,
            InstrumentType = OkxInstrumentType.Spot,
            Period = "1D"
        });
        _ = await api.TradingStatistics.GetContractTakerVolumeAsync(ExampleSwapInstrumentId);
        _ = await api.TradingStatistics.GetMarginLendingRatioAsync(ExampleCurrency, "1D");
        _ = await api.TradingStatistics.GetTopTradersContractLongShortRatioAsync(ExampleSwapInstrumentId);
        _ = await api.TradingStatistics.GetTopTradersContractLongShortRatioByPositionAsync(ExampleSwapInstrumentId);
        _ = await api.TradingStatistics.GetContractLongShortRatioAsync(ExampleSwapInstrumentId);
        _ = await api.TradingStatistics.GetLongShortRatioAsync(ExampleCurrency, "1D");
        _ = await api.TradingStatistics.GetContractSummaryAsync(ExampleCurrency, "1D");
        _ = await api.TradingStatistics.GetOptionsSummaryAsync(ExampleCurrency, "1D");
        _ = await api.TradingStatistics.GetPutCallRatioAsync(new OkxRubikOptionPeriodRequest
        {
            Currency = ExampleCurrency,
            Period = "1D"
        });
        _ = await api.TradingStatistics.GetInterestVolumeExpiryAsync(ExampleCurrency, "1D");
        _ = await api.TradingStatistics.GetInterestVolumeStrikeAsync(ExampleCurrency, "20240628", "1D");
        _ = await api.TradingStatistics.GetTakerFlowAsync(ExampleCurrency, "1D");
    }
    #endregion

    #region REST - Funding Account
    private static async Task FundingRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.Funding.GetCurrenciesAsync();
        _ = await api.Funding.GetBalancesAsync();
        _ = await api.Funding.GetNonTradableBalancesAsync();
        _ = await api.Funding.GetAssetValuationAsync();
        _ = await api.Funding.TransferAsync(OkxFundingTransferType.TransferWithinAccount, ExampleQuoteCurrency, 25m, OkxAccount.Funding, OkxAccount.Trading);
        _ = await api.Funding.TransferStateAsync();
        _ = await api.Funding.GetBillsAsync(ExampleQuoteCurrency);
        _ = await api.Funding.GetDepositAddressAsync(ExampleCurrency);
        _ = await api.Funding.GetDepositHistoryAsync(new OkxFundingDepositHistoryRequest
        {
            Currency = ExampleQuoteCurrency,
            Limit = 20
        });
        _ = await api.Funding.WithdrawAsync(ExampleQuoteCurrency, 100m, OkxFundingWithdrawalDestination.DigitalCurrencyAddress, "replace-with-address", OkxFundingWithdrawalAddressType.AddressInfo, "USDT-TRC20");
        _ = await api.Funding.CancelWithdrawalAsync(ExampleOrderId);
        _ = await api.Funding.GetWithdrawalHistoryAsync(new OkxFundingWithdrawalHistoryRequest
        {
            Currency = ExampleQuoteCurrency,
            Limit = 20
        });
        _ = await api.Funding.GetExchangesAsync();
        _ = await api.Funding.ApplyForMonthlyStatementAsync();
        _ = await api.Funding.GetMonthlyStatementAsync("replace-with-month");
        _ = await api.Funding.GetConvertCurrenciesAsync();
        _ = await api.Funding.GetConvertCurrencyPairAsync(ExampleCurrency, ExampleQuoteCurrency);
        _ = await api.Funding.EstimateQuoteAsync(new OkxFundingConvertEstimateQuoteRequest
        {
            BaseCurrency = ExampleCurrency,
            QuoteCurrency = ExampleQuoteCurrency,
            Side = OkxTradeOrderSide.Sell,
            RfqAmount = 0.01m,
            RfqCurrency = ExampleCurrency
        });
        _ = await api.Funding.PlaceConvertOrderAsync("replace-with-quote-id", ExampleCurrency, ExampleQuoteCurrency, OkxTradeOrderSide.Sell, 0.01m, ExampleCurrency);
        _ = await api.Funding.GetConvertHistoryAsync();
    }
    #endregion

    #region REST - Sub Account
    private static async Task SubAccountRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.SubAccount.GetSubAccountsAsync(new OkxSubAccountListRequest
        {
            Enable = true,
            Limit = 20
        });
        _ = await api.SubAccount.GetSubAccountTradingBalancesAsync(ExampleSubAccountName);
        _ = await api.SubAccount.GetSubAccountFundingBalancesAsync(ExampleSubAccountName);
        _ = await api.SubAccount.GetSubAccountMaximumWithdrawalsAsync(ExampleSubAccountName);
        _ = await api.SubAccount.GetSubAccountBillsAsync();
        _ = await api.SubAccount.GetSubAccountManagedBillsAsync();
        _ = await api.SubAccount.TransferBetweenSubAccountsAsync(ExampleCurrency, 0.5m, OkxAccount.Funding, OkxAccount.Trading, "from-sub-account", "to-sub-account");
        _ = await api.SubAccount.SetPermissionOfTransferOutAsync(ExampleSubAccountName, true);
        _ = await api.SubAccount.GetCustodySubAccountsAsync();
    }
    #endregion

    #region REST - Financial Products
    private static async Task FinancialRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        // On-chain earn
        _ = await api.Financial.OnChainEarn.GetOffersAsync();
        _ = await api.Financial.OnChainEarn.PurchaseAsync(ExampleProductId, []);
        _ = await api.Financial.OnChainEarn.RedeemAsync(ExampleProductId, "replace-with-order-id");
        _ = await api.Financial.OnChainEarn.CancelAsync(ExampleProductId, "replace-with-order-id");
        _ = await api.Financial.OnChainEarn.GetOpenOrdersAsync();
        _ = await api.Financial.OnChainEarn.GetHistoryAsync();

        // ETH staking
        _ = await api.Financial.EthStaking.GetProductInfoAsync();
        _ = await api.Financial.EthStaking.PurchaseAsync(0.1m);
        _ = await api.Financial.EthStaking.RedeemAsync(0.1m);
        _ = await api.Financial.EthStaking.CancelRedeemAsync("replace-with-redeem-id");
        _ = await api.Financial.EthStaking.GetBalancesAsync();
        _ = await api.Financial.EthStaking.GetHistoryAsync(OkxFinancialStakingType.Purchase);
        _ = await api.Financial.EthStaking.GetApyHistoryAsync(30);

        // SOL staking
        _ = await api.Financial.SolStaking.GetProductInfoAsync();
        _ = await api.Financial.SolStaking.PurchaseAsync(0.1m);
        _ = await api.Financial.SolStaking.RedeemAsync(0.1m);
        _ = await api.Financial.SolStaking.GetBalancesAsync();
        _ = await api.Financial.SolStaking.GetHistoryAsync(OkxFinancialStakingType.Purchase);
        _ = await api.Financial.SolStaking.GetApyHistoryAsync(30);

        // Simple Earn
        _ = await api.Financial.SimpleEarn.GetBalancesAsync();
        _ = await api.Financial.SimpleEarn.PurchaseAsync(ExampleQuoteCurrency, 100m, 0.05m);
        _ = await api.Financial.SimpleEarn.RedeemAsync(ExampleQuoteCurrency, 100m, 0.05m);
        _ = await api.Financial.SimpleEarn.SetLendingRateAsync(ExampleQuoteCurrency, 0.05m);
        _ = await api.Financial.SimpleEarn.GetLendingHistoryAsync();
        _ = await api.Financial.SimpleEarn.GetPublicBorrowSummaryAsync(ExampleCurrency);
        _ = await api.Financial.SimpleEarn.GetPublicBorrowHistoryAsync(ExampleCurrency);

        // Flexible loan
        _ = await api.Financial.FlexibleLoan.GetBorrowableCurrenciesAsync();

        // Dual investment
        _ = await api.Financial.DualInvestment.GetCurrencyPairsAsync();
        _ = await api.Financial.DualInvestment.RequestQuoteAsync(ExampleProductId, 100m, ExampleQuoteCurrency);
    }
    #endregion

    #region REST - Affiliate / Broker
    private static async Task AffiliateRestReferenceAsync()
    {
        var api = CreatePrivateRestClient();

        _ = await api.Affiliate.GetInviteeAsync(ExampleInviteeUid);
        _ = await api.Affiliate.GetRebateInformationAsync("replace-with-invitee-api-key");
    }

    private static void BrokerRestReference()
    {
        var api = CreatePrivateRestClient();

        // Broker is exposed as nested clients.
        // Current public surface:
        var fullyDisclosedBroker = api.Broker.FD;
        var dmaBroker = api.Broker.DMA;

        _ = fullyDisclosedBroker;
        _ = dmaBroker;
    }
    #endregion

    #region WebSocket - Public
    private static async Task PublicWebSocketReferenceAsync()
    {
        var ws = CreatePublicWebSocketClient();

        var tickersSubscription = await ws.Public.SubscribeToTickersAsync(data =>
        {
            Console.WriteLine($"Ticker {data.InstrumentId} Ask:{data.AskPrice} Bid:{data.BidPrice}");
        }, ExampleInstrumentId);

        _ = await ws.Public.SubscribeToInstrumentsAsync(_ => { }, OkxInstrumentType.Spot);
        _ = await ws.Public.SubscribeToOpenInterestsAsync(_ => { }, ExampleSwapInstrumentId);
        _ = await ws.Public.SubscribeToFundingRatesAsync(_ => { }, ExampleSwapInstrumentId);
        _ = await ws.Public.SubscribeToPriceLimitAsync(_ => { }, ExampleSwapInstrumentId);
        _ = await ws.Public.SubscribeToOptionSummaryAsync(_ => { }, ExampleOptionFamily);
        _ = await ws.Public.SubscribeToEstimatedPriceAsync(_ => { }, OkxInstrumentType.Option);
        _ = await ws.Public.SubscribeToMarkPriceAsync(_ => { }, ExampleSwapInstrumentId);
        _ = await ws.Public.SubscribeToIndexTickersAsync(_ => { }, "BTC-USDT");
        _ = await ws.Public.SubscribeToCandlesticksAsync(_ => { }, ExampleInstrumentId, OkxPeriod.FiveMinutes);
        _ = await ws.Public.SubscribeToTradesAsync(_ => { }, ExampleInstrumentId);
        _ = await ws.Public.SubscribeToOrderBookAsync(_ => { }, ExampleInstrumentId, OkxOrderBookType.OrderBook);
        _ = await ws.Public.SubscribeToSystemUpgradeStatusAsync(_ => { });

        await ws.UnsubscribeAsync(tickersSubscription.Data);
    }
    #endregion

    #region WebSocket - Private
    private static async Task PrivateWebSocketReferenceAsync()
    {
        var ws = CreatePrivateWebSocketClient();

        // Trading Account channels
        _ = await ws.Account.SubscribeToAccountUpdatesAsync(_ => { });
        _ = await ws.Account.SubscribeToPositionUpdatesAsync(_ => { }, OkxInstrumentType.Futures, ExampleFutureFamily, ExampleFutureInstrumentId);
        _ = await ws.Account.SubscribeToBalanceAndPositionUpdatesAsync(_ => { });
        _ = await ws.Account.SubscribeToPositionRiskUpdatesAsync(OkxInstrumentType.Futures, _ => { });
        _ = await ws.Account.SubscribeToAccountGreeksUpdatesAsync(_ => { });

        // Trade channels and trading operations
        _ = await ws.Trade.SubscribeToOrderUpdatesAsync(_ => { }, OkxInstrumentType.Futures, ExampleFutureFamily, ExampleFutureInstrumentId);
        _ = await ws.Trade.SubscribeToFillsAsync(_ => { });
        _ = await ws.Trade.PlaceOrderAsync(new OkxTradeOrderPlaceRequest
        {
            InstrumentIdCode = 2021032621966107,
            TradeMode = OkxTradeMode.Cash,
            OrderSide = OkxTradeOrderSide.Buy,
            PositionSide = OkxTradePositionSide.Net,
            OrderType = OkxTradeOrderType.MarketOrder,
            Size = 0.001m
        });
        _ = await ws.Trade.CancelOrderAsync(new OkxTradeOrderCancelRequest
        {
            InstrumentIdCode = 2021032621966107,
            OrderId = ExampleOrderId
        });
        _ = await ws.Trade.AmendOrderAsync(new OkxTradeOrderAmendRequest
        {
            InstrumentIdCode = 2021032621966107,
            OrderId = ExampleOrderId,
            NewQuantity = "0.002"
        });

        // Algo / recurring buy / spread channels
        _ = await ws.Algo.SubscribeToAlgoOrderUpdatesAsync(_ => { }, OkxInstrumentType.Futures, ExampleFutureFamily, ExampleFutureInstrumentId);
        _ = await ws.Algo.SubscribeToAdvanceAlgoOrderUpdatesAsync(_ => { }, OkxInstrumentType.Futures, ExampleFutureFamily, ExampleFutureInstrumentId);
        _ = await ws.RecurringBuy.SubscribeToOrderUpdatesAsync(_ => { }, OkxInstrumentType.Spot);
        _ = await ws.Spread.SubscribeToOrderUpdatesAsync(_ => { });
        _ = await ws.Spread.SubscribeToTradeUpdatesAsync(_ => { });
    }
    #endregion

    private static OkxRestApiClient CreatePublicRestClient()
    {
        return new OkxRestApiClient(new OkxRestApiOptions
        {
            SignPublicRequests = false,
            AutoTimestamp = true,
            AutoTimestampInterval = TimeSpan.FromHours(1)
        });
    }

    private static OkxRestApiClient CreatePrivateRestClient(bool demoTrading = false, bool signPublicRequests = false)
    {
        var api = new OkxRestApiClient(new OkxRestApiOptions
        {
            DemoTradingService = demoTrading,
            SignPublicRequests = signPublicRequests,
            AutoTimestamp = true,
            AutoTimestampInterval = TimeSpan.FromHours(1)
        });

        api.SetApiCredentials(ExampleApiKey, ExampleApiSecret, ExampleApiPassphrase);
        return api;
    }

    private static OkxWebSocketApiClient CreatePublicWebSocketClient()
    {
        return new OkxWebSocketApiClient(new OkxWebSocketApiOptions());
    }

    private static OkxWebSocketApiClient CreatePrivateWebSocketClient(bool demoTrading = false)
    {
        var ws = new OkxWebSocketApiClient(new OkxWebSocketApiOptions
        {
            DemoTradingService = demoTrading
        });

        ws.SetApiCredentials(ExampleApiKey, ExampleApiSecret, ExampleApiPassphrase);
        return ws;
    }
}
