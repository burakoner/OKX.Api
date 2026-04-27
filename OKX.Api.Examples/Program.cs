namespace OKX.Api.Examples;

internal static class Program
{
    private const string ExampleInstrumentId = "BTC-USDT";
    private const string ExampleSwapInstrumentId = "BTC-USDT-SWAP";
    private const string ExampleInstrumentFamily = "BTC-USDT";
    private const string ExampleFutureFamily = "BTC-USD";
    private const string ExampleSpreadId = "BTC-USDT_BTC-USD-SWAP";
    private const string ExampleEventSeriesId = "replace-with-series-id";
    private const string ExampleEventId = "replace-with-event-id";
    private const long ExampleOrderId = 123456789L;
    private const long ExampleAlgoOrderId = 123456789L;
    private const long ExampleSignalChannelId = 123456789L;

    private static async Task Main(string[] args)
    {
        var api = CreateRestClient();

        WriteHeader("OKX.Api Examples");
        Console.WriteLine("This example program is organized into runnable read-only samples and opt-in templates for account-changing operations.");
        Console.WriteLine();

        await RunPublicRestExamplesAsync(api).ConfigureAwait(false);
        await RunTradingStatisticsExamplesAsync(api).ConfigureAwait(false);
        await RunPublicBlockAndSpreadExamplesAsync(api).ConfigureAwait(false);

        if (TrySetApiCredentials(api))
        {
            await RunPrivateReadOnlyExamplesAsync(api).ConfigureAwait(false);
        }
        else
        {
            Console.WriteLine("Skipping private REST examples because OKX_API_KEY / OKX_API_SECRET / OKX_API_PASSPHRASE are not set.");
            Console.WriteLine();
        }

        if (IsEnabled("OKX_EXAMPLES_RUN_WEBSOCKET"))
        {
            await RunWebSocketExamplesAsync().ConfigureAwait(false);
        }
        else
        {
            Console.WriteLine("WebSocket examples are disabled. Set OKX_EXAMPLES_RUN_WEBSOCKET=true to run them.");
            Console.WriteLine();
        }

        WriteHeader("Template Sections");
        Console.WriteLine("The methods below are intentionally not invoked from Main because they create, amend, or cancel live resources.");
        Console.WriteLine("Open this file and adapt them before using them on a funded account:");
        Console.WriteLine("- RunTradeMutationTemplatesAsync");
        Console.WriteLine("- RunAlgoAndBotMutationTemplatesAsync");
        Console.WriteLine("- RunFundingAndFinancialMutationTemplatesAsync");
        Console.WriteLine();
    }

    private static OkxRestApiClient CreateRestClient()
    {
        var options = new OkxRestApiOptions
        {
            DemoTradingService = IsEnabled("OKX_DEMO_TRADING"),
            SignPublicRequests = false,
            AutoTimestamp = true,
            AutoTimestampInterval = TimeSpan.FromHours(1)
        };

        return new OkxRestApiClient(options);
    }

    private static OkxWebSocketApiClient CreateWebSocketClient()
    {
        var options = new OkxWebSocketApiOptions
        {
            DemoTradingService = IsEnabled("OKX_DEMO_TRADING")
        };

        return new OkxWebSocketApiClient(options);
    }

    private static bool TrySetApiCredentials(OkxRestApiClient api)
    {
        var apiKey = Environment.GetEnvironmentVariable("OKX_API_KEY");
        var apiSecret = Environment.GetEnvironmentVariable("OKX_API_SECRET");
        var passphrase = Environment.GetEnvironmentVariable("OKX_API_PASSPHRASE");

        if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret) || string.IsNullOrWhiteSpace(passphrase))
            return false;

        api.SetApiCredentials(apiKey, apiSecret, passphrase);
        return true;
    }

    private static bool TrySetApiCredentials(OkxWebSocketApiClient api)
    {
        var apiKey = Environment.GetEnvironmentVariable("OKX_API_KEY");
        var apiSecret = Environment.GetEnvironmentVariable("OKX_API_SECRET");
        var passphrase = Environment.GetEnvironmentVariable("OKX_API_PASSPHRASE");

        if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret) || string.IsNullOrWhiteSpace(passphrase))
            return false;

        api.SetApiCredentials(apiKey, apiSecret, passphrase);
        return true;
    }

    private static async Task RunPublicRestExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Public");

        await LogRestCallAsync("Public.GetServerTimeAsync", api.Public.GetServerTimeAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetTickersAsync", api.Public.GetTickersAsync(OkxInstrumentType.Spot)).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetTickerAsync", api.Public.GetTickerAsync(ExampleInstrumentId)).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetOrderBookAsync", api.Public.GetOrderBookAsync(ExampleInstrumentId, 40)).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetCandlesticksAsync(request)", api.Public.GetCandlesticksAsync(new OkxPublicCandlestickRequest
        {
            InstrumentId = ExampleInstrumentId,
            Period = "1H",
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetInstrumentsAsync(request)", api.Public.GetInstrumentsAsync(new OkxPublicInstrumentQueryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentFamily = ExampleInstrumentFamily
        })).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetFundingRateHistoryAsync(request)", api.Public.GetFundingRateHistoryAsync(new OkxPublicFundingRateHistoryRequest
        {
            InstrumentId = ExampleSwapInstrumentId,
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetEconomicCalendarDataAsync(request)", api.Public.GetEconomicCalendarDataAsync(new OkxPublicEconomicCalendarRequest
        {
            Region = "united_states",
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Public.GetEventContractSeriesAsync", api.Public.GetEventContractSeriesAsync()).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunTradingStatisticsExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Trading Statistics");

        await LogRestCallAsync("TradingStatistics.GetSupportCoinAsync", api.TradingStatistics.GetSupportCoinAsync()).ConfigureAwait(false);
        await LogRestCallAsync("TradingStatistics.GetContractOpenInterestHistoryAsync(request)", api.TradingStatistics.GetContractOpenInterestHistoryAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = ExampleSwapInstrumentId,
            Period = "1D",
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("TradingStatistics.GetTakerVolumeAsync(request)", api.TradingStatistics.GetTakerVolumeAsync(new OkxRubikTakerVolumeRequest
        {
            Currency = "BTC",
            InstrumentType = OkxInstrumentType.Spot,
            Period = "1D"
        })).ConfigureAwait(false);
        await LogRestCallAsync("TradingStatistics.GetPutCallRatioAsync(request)", api.TradingStatistics.GetPutCallRatioAsync(new OkxRubikOptionPeriodRequest
        {
            Currency = "BTC",
            Period = "1D"
        })).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunPublicBlockAndSpreadExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Public Block / Spread");

        await LogRestCallAsync("Block.GetTickersAsync", api.Block.GetTickersAsync(OkxInstrumentType.Futures)).ConfigureAwait(false);
        await LogRestCallAsync("Block.GetPublicRecentTradesAsync", api.Block.GetPublicRecentTradesAsync(ExampleInstrumentId)).ConfigureAwait(false);
        await LogRestCallAsync("Spread.GetSpreadsAsync(request)", api.Spread.GetSpreadsAsync(new OkxSpreadInstrumentQueryRequest
        {
            BaseCurrency = "BTC"
        })).ConfigureAwait(false);
        await LogRestCallAsync("Spread.GetTickerAsync", api.Spread.GetTickerAsync(ExampleSpreadId)).ConfigureAwait(false);
        await LogRestCallAsync("Spread.GetCandlesticksAsync(request)", api.Spread.GetCandlesticksAsync(new OkxSpreadCandlestickRequest
        {
            SpreadId = ExampleSpreadId,
            Period = "1H",
            Limit = 20
        })).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunPrivateReadOnlyExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Private Read-Only");

        await RunAccountExamplesAsync(api).ConfigureAwait(false);
        await RunTradeExamplesAsync(api).ConfigureAwait(false);
        await RunAlgoGridAndDcaExamplesAsync(api).ConfigureAwait(false);
        await RunSignalRecurringCopyExamplesAsync(api).ConfigureAwait(false);
        await RunFundingExamplesAsync(api).ConfigureAwait(false);
        await RunSubAccountExamplesAsync(api).ConfigureAwait(false);
        await RunFinancialExamplesAsync(api).ConfigureAwait(false);
        await RunAffiliateExamplesAsync(api).ConfigureAwait(false);
    }

    private static async Task RunAccountExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Account");

        await LogRestCallAsync("Account.GetBalancesAsync", api.Account.GetBalancesAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Account.GetPositionsAsync", api.Account.GetPositionsAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Account.GetPositionsHistoryAsync(request)", api.Account.GetPositionsHistoryAsync(new OkxAccountPositionsHistoryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentId = ExampleSwapInstrumentId,
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Account.GetConfigurationAsync", api.Account.GetConfigurationAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Account.GetGreeksAsync", api.Account.GetGreeksAsync()).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunTradeExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Trade");

        await LogRestCallAsync("Trade.GetOpenOrdersAsync(request)", api.Trade.GetOpenOrdersAsync(new OkxTradeOpenOrdersRequest
        {
            InstrumentType = OkxInstrumentType.Spot,
            InstrumentId = ExampleInstrumentId,
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Trade.GetOrderHistoryAsync(request)", api.Trade.GetOrderHistoryAsync(new OkxTradeOrderQueryRequest
        {
            InstrumentType = OkxInstrumentType.Spot,
            InstrumentId = ExampleInstrumentId,
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Trade.GetTradesHistoryAsync(request)", api.Trade.GetTradesHistoryAsync(new OkxTradeTransactionQueryRequest
        {
            InstrumentType = OkxInstrumentType.Spot,
            InstrumentId = ExampleInstrumentId,
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Trade.GetAccountRateLimitAsync", api.Trade.GetAccountRateLimitAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Trade.OrderPrecheckAsync(request)", api.Trade.OrderPrecheckAsync(new OkxTradeOrderPrecheckRequest
        {
            InstrumentId = ExampleInstrumentId,
            TradeMode = OkxTradeMode.Cash,
            OrderSide = OkxTradeOrderSide.Buy,
            OrderType = OkxTradeOrderType.LimitOrder,
            Size = 0.001m,
            Price = 1000m
        })).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunAlgoGridAndDcaExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Algo / Grid / DCA");

        await LogRestCallAsync("Algo.GetOpenOrdersAsync", api.Algo.GetOpenOrdersAsync(OkxAlgoOrderType.Conditional)).ConfigureAwait(false);
        await LogRestCallAsync("Grid.GetOrdersHistoryAsync", api.Grid.GetOrdersHistoryAsync(OkxGridAlgoOrderType.SpotGrid)).ConfigureAwait(false);
        await LogRestCallAsync("Grid.GetAIParameterAsync", api.Grid.GetAIParameterAsync(OkxGridAlgoOrderType.SpotGrid, ExampleInstrumentId)).ConfigureAwait(false);
        await LogRestCallAsync("DCA.GetOpenOrdersAsync", api.DCA.GetOpenOrdersAsync(OkxDcaAlgoOrderType.SpotDca)).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunSignalRecurringCopyExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Signal Bot / Recurring Buy / Copy Trading");

        await LogRestCallAsync("SignalBot.GetChannelsAsync", api.SignalBot.GetChannelsAsync(OkxSignalBotSourceType.FreeSignal)).ConfigureAwait(false);
        await LogRestCallAsync("RecurringBuy.GetOpenOrdersAsync", api.RecurringBuy.GetOpenOrdersAsync()).ConfigureAwait(false);
        await LogRestCallAsync("CopyTrading.GetLeadTradersRanksAsync", api.CopyTrading.GetLeadTradersRanksAsync()).ConfigureAwait(false);
        await LogRestCallAsync("CopyTrading.GetPublicConfigurationAsync", api.CopyTrading.GetPublicConfigurationAsync()).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunFundingExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Funding");

        await LogRestCallAsync("Funding.GetBalancesAsync", api.Funding.GetBalancesAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Funding.GetDepositAddressAsync", api.Funding.GetDepositAddressAsync("BTC")).ConfigureAwait(false);
        await LogRestCallAsync("Funding.GetDepositHistoryAsync(request)", api.Funding.GetDepositHistoryAsync(new OkxFundingDepositHistoryRequest
        {
            Currency = "BTC",
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("Funding.GetConvertCurrenciesAsync", api.Funding.GetConvertCurrenciesAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Funding.EstimateQuoteAsync(request)", api.Funding.EstimateQuoteAsync(new OkxFundingConvertEstimateQuoteRequest
        {
            BaseCurrency = "BTC",
            QuoteCurrency = "USDT",
            Side = OkxTradeOrderSide.Sell,
            RfqAmount = 0.01m,
            RfqCurrency = "BTC"
        })).ConfigureAwait(false);
        await LogRestCallAsync("Funding.GetWithdrawalHistoryAsync(request)", api.Funding.GetWithdrawalHistoryAsync(new OkxFundingWithdrawalHistoryRequest
        {
            Currency = "USDT",
            Limit = 20
        })).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunSubAccountExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Sub Account");

        await LogRestCallAsync("SubAccount.GetSubAccountsAsync(request)", api.SubAccount.GetSubAccountsAsync(new OkxSubAccountListRequest
        {
            Enable = true,
            Limit = 20
        })).ConfigureAwait(false);
        await LogRestCallAsync("SubAccount.GetCustodySubAccountsAsync", api.SubAccount.GetCustodySubAccountsAsync()).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunFinancialExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Financial");

        await LogRestCallAsync("Financial.OnChainEarn.GetOffersAsync", api.Financial.OnChainEarn.GetOffersAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Financial.EthStaking.GetProductInfoAsync", api.Financial.EthStaking.GetProductInfoAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Financial.SolStaking.GetProductInfoAsync", api.Financial.SolStaking.GetProductInfoAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Financial.SimpleEarn.GetPublicBorrowSummaryAsync", api.Financial.SimpleEarn.GetPublicBorrowSummaryAsync("BTC")).ConfigureAwait(false);
        await LogRestCallAsync("Financial.SimpleEarn.GetPublicBorrowHistoryAsync", api.Financial.SimpleEarn.GetPublicBorrowHistoryAsync("BTC")).ConfigureAwait(false);
        await LogRestCallAsync("Financial.FlexibleLoan.GetBorrowableCurrenciesAsync", api.Financial.FlexibleLoan.GetBorrowableCurrenciesAsync()).ConfigureAwait(false);
        await LogRestCallAsync("Financial.DualInvestment.GetCurrencyPairsAsync", api.Financial.DualInvestment.GetCurrencyPairsAsync()).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunAffiliateExamplesAsync(OkxRestApiClient api)
    {
        WriteHeader("REST - Affiliate");

        await LogRestCallAsync("Affiliate.GetInviteeAsync", api.Affiliate.GetInviteeAsync(1_000_000L)).ConfigureAwait(false);

        Console.WriteLine();
    }

    private static async Task RunWebSocketExamplesAsync()
    {
        WriteHeader("WebSocket");

        var ws = CreateWebSocketClient();
        var hasCredentials = TrySetApiCredentials(ws);

        var tickerSubscription = await ws.Public.SubscribeToTickersAsync(data =>
        {
            Console.WriteLine($"Ticker update {data.InstrumentId} Ask:{data.AskPrice} Bid:{data.BidPrice}");
        }, ExampleInstrumentId).ConfigureAwait(false);

        if (!tickerSubscription.Success)
        {
            Console.WriteLine($"[FAIL] Public.SubscribeToTickersAsync: {tickerSubscription.Error}");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("[ OK ] Public.SubscribeToTickersAsync");

        if (hasCredentials)
        {
            var accountSubscription = await ws.Account.SubscribeToAccountUpdatesAsync(data =>
            {
                Console.WriteLine($"Account update received. Details: {DescribeData(data)}");
            }).ConfigureAwait(false);

            if (accountSubscription.Success)
                Console.WriteLine("[ OK ] Account.SubscribeToAccountUpdatesAsync");
            else
                Console.WriteLine($"[FAIL] Account.SubscribeToAccountUpdatesAsync: {accountSubscription.Error}");
        }
        else
        {
            Console.WriteLine("Skipping private WebSocket examples because credentials are not configured.");
        }

        Console.WriteLine("Listening for WebSocket updates for 10 seconds...");
        await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);

        await ws.UnsubscribeAsync(tickerSubscription.Data).ConfigureAwait(false);
        Console.WriteLine("[ OK ] Public.UnsubscribeAsync");
        Console.WriteLine();
    }

    // These template sections are intentionally not executed from Main.
    // They are here as structured starting points for account-changing flows.

    private static async Task RunTradeMutationTemplatesAsync(OkxRestApiClient api)
    {
        await api.Trade.PlaceOrderAsync(ExampleInstrumentId, OkxTradeMode.Cash, OkxTradeOrderSide.Buy, OkxTradePositionSide.Net, OkxTradeOrderType.MarketOrder, 0.001m);
        await api.Trade.CancelOrderAsync(ExampleInstrumentId, orderId: ExampleOrderId);
        await api.Trade.AmendOrderAsync(ExampleInstrumentId, orderId: ExampleOrderId, newQuantity: 0.002m);
        await api.Trade.CancelAllAfterAsync(30);

        await api.Spread.PlaceOrderAsync(ExampleSpreadId, OkxTradeOrderSide.Buy, OkxSpreadOrderType.LimitOrder, 1m, 10m);
        await api.Block.CancelAllQuotesAfterAsync(30);
    }

    private static async Task RunAlgoAndBotMutationTemplatesAsync(OkxRestApiClient api)
    {
        await api.Algo.PlaceOrderAsync(ExampleInstrumentId, OkxTradeMode.Cash, OkxTradeOrderSide.Buy, OkxAlgoOrderType.Conditional);

        await api.Grid.PlaceOrderAsync(new OkxGridPlaceOrderRequest
        {
            InstrumentId = ExampleInstrumentId,
            AlgoOrderType = OkxGridAlgoOrderType.SpotGrid,
            MaximumPrice = 50000m,
            MinimumPrice = 40000m,
            GridNumber = 10,
            GridRunType = OkxGridRunType.Arithmetic,
            QuoteSize = 100m
        });

        await api.DCA.PlaceOrderAsync(new OkxDcaCreateOrderRequest
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

        await api.SignalBot.CreateSignalBotAsync(
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

        await api.RecurringBuy.PlaceOrderAsync(
            "Example recurring buy",
            [],
            OkxRecurringBuyPeriod.Monthly,
            1,
            null,
            1,
            "UTC",
            100m,
            "USDT",
            OkxTradeMode.Cash);
    }

    private static async Task RunFundingAndFinancialMutationTemplatesAsync(OkxRestApiClient api)
    {
        await api.Funding.TransferAsync(OkxFundingTransferType.TransferWithinAccount, "USDT", 25m, OkxAccount.Funding, OkxAccount.Trading);

        await api.Financial.EthStaking.PurchaseAsync(0.1m);
        await api.Financial.SolStaking.PurchaseAsync(0.1m);
        await api.Financial.SimpleEarn.PurchaseAsync("USDT", 100m, 0.05m);
        await api.Financial.DualInvestment.RequestQuoteAsync("replace-with-product-id", 100m, "USDT");
    }

    private static async Task LogRestCallAsync<T>(string label, Task<RestCallResult<T>> task)
    {
        var result = await task.ConfigureAwait(false);

        if (!result.Success)
        {
            Console.WriteLine($"[FAIL] {label}: {result.Error}");
            return;
        }

        Console.WriteLine($"[ OK ] {label}: {DescribeData(result.Data)}");
    }

    private static string DescribeData<T>(T data)
    {
        if (data is null)
            return "no data";

        if (data is string value)
            return value;

        if (data is System.Collections.ICollection collection)
            return $"{collection.Count} item(s)";

        return data.GetType().Name;
    }

    private static void WriteHeader(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
    }

    private static bool IsEnabled(string environmentVariableName)
        => string.Equals(Environment.GetEnvironmentVariable(environmentVariableName), "true", StringComparison.OrdinalIgnoreCase);
}
