# OKX.Api

A .Net wrapper for the OKX API as described on [OKX](https://www.okx.com/docs-v5/en/), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/burakoner/OKX.Api/issues)**

## Donations
Donations are greatly appreciated and a motivation to keep improving.

**BTC**:  33WbRKqt7wXARVdAJSu1G1x3QnbyPtZ2bH  
**ETH**:  0x65b02db9b67b73f5f1e983ae10796f91ded57b64  


## Installation
![Nuget version](https://img.shields.io/nuget/v/OKX.Api.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/OKX.Api.svg)
Available on [Nuget](https://www.nuget.org/packages/OKX.Api).
```
PM> Install-Package OKX.Api
```
To get started with OKX.Api first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/OKX.Api). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'OKX.Api' and hit enter. The OKX.Api package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use OKX.Api in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that OKX.Api will be installed in. After selecting the correct project type  `Install-Package OKX.Api`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using OKX.Api.

## Getting started
After installing it's time to actually use it. To get started we have to add the OKX.Api namespace:  `using OKX.Api;`.

OKX.Api provides two clients to interact with the OKX.Api. The  `OKXRestApiClient`  provides all rest API calls. The  `OKXStreamClient` provides functions to interact with the websocket provided by the OKX.Api. Both clients are disposable and as such can be used in a  `using`statement.

## Rest Api Examples
**Public Endpoints (Unsigned)**
```csharp
var api = new OKXRestApiClient();
var public_01 = await api.PublicData.GetInstrumentsAsync(OkxInstrumentType.Spot);
var public_02 = await api.PublicData.GetInstrumentsAsync(OkxInstrumentType.Margin);
var public_03 = await api.PublicData.GetInstrumentsAsync(OkxInstrumentType.Swap);
var public_04 = await api.PublicData.GetInstrumentsAsync(OkxInstrumentType.Futures);
var public_05 = await api.PublicData.GetInstrumentsAsync(OkxInstrumentType.Option, "USD");
var public_06 = await api.PublicData.GetDeliveryExerciseHistoryAsync(OkxInstrumentType.Futures, "BTC-USD");
var public_07 = await api.PublicData.GetDeliveryExerciseHistoryAsync(OkxInstrumentType.Option, "BTC-USD");
var public_08 = await api.PublicData.GetOpenInterestsAsync(OkxInstrumentType.Futures);
var public_09 = await api.PublicData.GetOpenInterestsAsync(OkxInstrumentType.Option, "BTC-USD");
var public_10 = await api.PublicData.GetOpenInterestsAsync(OkxInstrumentType.Swap, "BTC-USD");
var public_11 = await api.PublicData.GetFundingRatesAsync("BTC-USD-SWAP");
var public_12 = await api.PublicData.GetFundingRateHistoryAsync("BTC-USD-SWAP");
var public_13 = await api.PublicData.GetLimitPriceAsync("BTC-USD-SWAP");
var public_14 = await api.PublicData.GetOptionMarketDataAsync("BTC-USD");
var public_15 = await api.PublicData.GetEstimatedPriceAsync("BTC-USD-211004-41000-C");
var public_16 = await api.PublicData.GetDiscountInfoAsync();
var public_17 = await api.PublicData.GetServerTimeAsync();
var public_19 = await api.PublicData.GetMarkPricesAsync(OkxInstrumentType.Futures);
var public_20 = await api.PublicData.GetPositionTiersAsync(OkxInstrumentType.Futures, OkxMarginMode.Isolated, "BTC-USD");
var public_21 = await api.PublicData.GetInterestRatesAsync();
var public_22 = await api.PublicData.GetVIPInterestRatesAsync();
var public_23 = await api.PublicData.GetUnderlyingAsync(OkxInstrumentType.Futures);
var public_24 = await api.PublicData.GetUnderlyingAsync(OkxInstrumentType.Option);
var public_25 = await api.PublicData.GetUnderlyingAsync(OkxInstrumentType.Swap);
var public_26 = await api.PublicData.GetInsuranceFundAsync(OkxInstrumentType.Margin, currency: "BTC");
var public_27 = await api.PublicData.UnitConvertAsync("BTC-USD-SWAP", price: 35000, size: 0.888m);
```

**Market Endpoints (Unsigned)**
```csharp
var api = new OKXRestApiClient();
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
```

**Rubik Endpoints (Unsigned)**
```csharp
var api = new OKXRestApiClient();
var rubik_01 = await api.TradingData.GetSupportCoinAsync();
var rubik_02 = await api.TradingData.GetTakerVolumeAsync("BTC", OkxInstrumentType.Spot);
var rubik_03 = await api.TradingData.GetMarginLendingRatioAsync("BTC", OkxPeriod.OneDay);
var rubik_04 = await api.TradingData.GetLongShortRatioAsync("BTC", OkxPeriod.OneDay);
var rubik_05 = await api.TradingData.GetContractSummaryAsync("BTC", OkxPeriod.OneDay);
var rubik_06 = await api.TradingData.GetOptionsSummaryAsync("BTC", OkxPeriod.OneDay);
var rubik_07 = await api.TradingData.GetPutCallRatioAsync("BTC", OkxPeriod.OneDay);
var rubik_08 = await api.TradingData.GetInterestVolumeExpiryAsync("BTC", OkxPeriod.OneDay);
var rubik_09 = await api.TradingData.GetInterestVolumeStrikeAsync("BTC", "20210623", OkxPeriod.OneDay);
var rubik_10 = await api.TradingData.GetTakerFlowAsync("BTC", OkxPeriod.OneDay);
```

**Wallet (Account) Endpoints (Signed)**
```csharp
var api = new OKXRestApiClient();
api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
var wallet_01 = await api.Account.GetAccountBalanceAsync();
var wallet_02 = await api.Account.GetAccountPositionsAsync();
var wallet_03 = await api.Account.GetAccountPositionsHistoryAsync();
var wallet_04 = await api.Account.GetAccountPositionRiskAsync();
var wallet_05 = await api.Account.GetBillHistoryAsync();
var wallet_06 = await api.Account.GetBillArchiveAsync();
var wallet_07 = await api.Account.GetAccountConfigurationAsync();
var wallet_08 = await api.Account.SetAccountPositionModeAsync(OkxPositionMode.LongShortMode);
var wallet_09 = await api.Account.GetAccountLeverageAsync("BTC-USD-211008", OkxMarginMode.Isolated);
var wallet_10 = await api.Account.SetAccountLeverageAsync(30, null, "BTC-USD-211008", OkxMarginMode.Isolated, OkxPositionSide.Long);
var wallet_11 = await api.Account.GetMaximumAmountAsync("BTC-USDT", OkxTradeMode.Isolated);
var wallet_12 = await api.Account.GetMaximumAvailableAmountAsync("BTC-USDT", OkxTradeMode.Isolated);
var wallet_13 = await api.Account.SetMarginAmountAsync("BTC-USDT", OkxPositionSide.Long, OkxMarginAddReduce.Add, 100.0m);
var wallet_14 = await api.Account.GetMaximumLoanAmountAsync("BTC-USDT", OkxMarginMode.Cross);
var wallet_15 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Spot);
var wallet_16 = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Futures);
var wallet_17 = await api.Account.GetInterestAccruedAsync();
var wallet_18 = await api.Account.GetInterestRateAsync();
var wallet_19 = await api.Account.SetGreeksAsync(OkxGreeksType.GreeksInCoins);
var wallet_20 = await api.Account.GetMaximumWithdrawalsAsync();
```

**SubAccount Endpoints (Signed)**
```csharp
var api = new OKXRestApiClient();
api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
var subaccount_01 = await api.SubAccount.GetSubAccountsAsync();
var subaccount_02 = await api.SubAccount.ResetSubAccountApiKeyAsync("subAccountName", "apiKey", "apiLabel", true, true, "");
var subaccount_03 = await api.SubAccount.GetSubAccountTradingBalancesAsync("subAccountName");
var subaccount_04 = await api.SubAccount.GetSubAccountFundingBalancesAsync("subAccountName");
var subaccount_05 = await api.SubAccount.GetSubAccountBillsAsync();
var subaccount_06 = await api.SubAccount.TransferBetweenSubAccountsAsync("BTC", 0.5m, OkxAccount.Funding, OkxAccount.Trading, "fromSubAccountName", "toSubAccountName");
```

**Funding Endpoints (Signed)**
```csharp
var api = new OKXRestApiClient();
api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
var funding_01 = await api.Funding.GetCurrenciesAsync();
var funding_02 = await api.Funding.GetFundingBalanceAsync();
var funding_03 = await api.Funding.FundTransferAsync("BTC", 0.5m, OkxTransferType.TransferWithinAccount, OkxAccount.Funding, OkxAccount.Trading);
var funding_04 = await api.Funding.GetFundingBillDetailsAsync("BTC");
var funding_05 = await api.Funding.GetLightningDepositsAsync("BTC", 0.001m);
var funding_06 = await api.Funding.GetDepositAddressAsync("BTC");
var funding_07 = await api.Funding.GetDepositAddressAsync("USDT");
var funding_08 = await api.Funding.GetDepositHistoryAsync("USDT");
var funding_09 = await api.Funding.WithdrawAsync("USDT", 100.0m, OkxWithdrawalDestination.DigitalCurrencyAddress, "toAddress", 1.0m, "USDT-TRC20");
var funding_10 = await api.Funding.GetLightningWithdrawalsAsync("BTC", "invoice", "password");
var funding_11 = await api.Funding.GetWithdrawalHistoryAsync("USDT");
var funding_12 = await api.Funding.GetSavingBalancesAsync();
var funding_13 = await api.Funding.SavingPurchaseRedemptionAsync("USDT", 10.0m, OkxSavingActionSide.Purchase);
```

**Trade Endpoints (Signed)**
```csharp
var api = new OKXRestApiClient();
api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
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
```

## Websocket Api Examples
The OKX.Api socket client provides several socket endpoint to which can be subscribed.

**Public Feeds**
```csharp
/* OKX Socket Client */
var ws = new OKXStreamClient();

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
        Console.WriteLine($"Instrument {data.Instrument} BaseCurrency:{data.BaseCurrency}");
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
```

**Private Feeds**
```csharp
/* OKX Socket Client */
var ws = new OKXStreamClient();
ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

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
```

## Release Notes
* Version 1.1.0 - 07 May 2023
    * Updated All Methods and Models, synced with OKX Api 2023-04-27 version

* Version 1.0.6 - 06 May 2023
    * Updated WithdrawAsync Method (https://github.com/burakoner/OKEx.Net/issues/97)
    * Updated GetInstrumentsAsync Method (https://github.com/burakoner/OKX.Api/issues/7)

* Version 1.0.0 - 25 Mar 2023
	* First Release
