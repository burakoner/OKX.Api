# OKX.Api

A .Net wrapper for the OKX API as described on [OKX](https://www.okx.com/docs-v5/en/), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/burakoner/OKX.Api/issues)**

## Donations

Donations are greatly appreciated and a motivation to keep improving.

**BTC**:  33WbRKqt7wXARVdAJSu1G1x3QnbyPtZ2bH  
**ETH**:  0x65b02db9b67b73f5f1e983ae10796f91ded57b64  
**USDT (TRC-20)**:  TXwqoD7doMESgitfWa8B2gHL7HuweMmNBJ  

## Installation

![Nuget version](https://img.shields.io/nuget/v/OKX.Api.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/OKX.Api.svg)
Available on [Nuget](https://www.nuget.org/packages/OKX.Api).

```console
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

```csharp
var api = new OKXRestApiClient();
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
var trade_09 = await api.Trading.GetOrdersAsync();
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
var algo_06 = await api.AlgoTrading.GetOrdersAsync(OkxAlgoOrderType.OCO);
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

// TODO: RecurringBuy Methods (Signed)

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
```

## WebSocket Api Examples

The OKX.Api socket client provides several socket endpoint to which can be subscribed.

```csharp
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
// TODO: RecurringBuy Updates (Private)
// TODO: CopyTrading Updates (Private)
// TODO: BlockTrading Updates (Private)
// TODO: SpreadTrading Updates (Private)
```

## Release Notes

* Version 2.0.0 - 22 Mar 2024
  * Changed main structure, edited tons of models, refactored tons of codes
  * Changed IEnumerable return types to List
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/58](https://github.com/burakoner/OKX.Api/pull/58)
  * Updated dependencies
  * Updated README file. Please refer to code snippets for samples

* Version 1.7.2 - 19 Mar 2024
  * Merged MarketData and PublicData clients

* Version 1.7.0 - 29 Feb 2024
  * Added WebSocket order management methods
  * Removed deprecated fields from request models
  * Released [https://github.com/burakoner/OKX.Api/issues/12](https://github.com/burakoner/OKX.Api/issues/12)

* Version 1.6.0 - 05 Feb 2024
  * ApiSharp updated to 2.1.0
  * Added feature [https://github.com/burakoner/OKX.Api/issues/56](https://github.com/burakoner/OKX.Api/issues/56)
  * Imported pull request [https://github.com/burakoner/OKX.Api/pull/55](https://github.com/burakoner/OKX.Api/pull/55)

* Version 1.5.6 - 03 Jan 2024
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/53](https://github.com/burakoner/OKX.Api/issues/53)

* Version 1.5.5 - 10 Dec 2023
  * Updated ApiSharp to 2.0.5
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/52](https://github.com/burakoner/OKX.Api/issues/52)
  * Refactored OKXRestApiTradingAccountClient and related models

* Version 1.5.1 - 15 Nov 2023
  * Updated ApiSharp to 2.0.1

* Version 1.5.0 - 13 Nov 2023
  * Fixed DemoTradingService usage
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/39](https://github.com/burakoner/OKX.Api/issues/39)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/48](https://github.com/burakoner/OKX.Api/issues/48)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/49](https://github.com/burakoner/OKX.Api/issues/49)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/50](https://github.com/burakoner/OKX.Api/issues/50)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/51](https://github.com/burakoner/OKX.Api/issues/51)

* Version 1.4.0 - 16 Sep 2023
  * Added Business WebSocket endpoint and moved related methods
  * Fixed websocket endpoint division (public, private, business) related issues
  * Some models changed (OkxPositionHistory)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/37](https://github.com/burakoner/OKX.Api/issues/37)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/40](https://github.com/burakoner/OKX.Api/issues/40)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/43](https://github.com/burakoner/OKX.Api/issues/43)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/46](https://github.com/burakoner/OKX.Api/issues/46)

* Version 1.3.1 - 06 Aug 2023
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/20](https://github.com/burakoner/OKX.Api/issues/20)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/36](https://github.com/burakoner/OKX.Api/pull/36)

* Version 1.3.0 - 06 Aug 2023
  * ApiSharp version updated to 1.5.0
  * Both Rest and Websocket Api client hierarchies synced with OKX Api Documentation
  * OKXStreamClient renamed to OKXWebSocketApiClient and methods moved to seperate clients according to OKX Api Documentation
  * Some method and parameter names changed
  * Timestamp conversion algorithm changed. You can now reach both timestamp and time properties
  * Added Copy Trading Section
  * Added OrderBookTrading.AlgoTrading.AmendAlgoOrderAsync (api/v5/trade/amend-algos)
  * Added OrderBookTrading.AlgoTrading.GetAlgoOrderDetailsAsync (api/v5/trade/order-algo)
  * Moved some MarketData methods to PublicData section: GetIndexCandlesticksAsync, GetMarkPriceCandlesticksAsync, GetIndexTickersAsync, GetOracleAsync, GetIndexComponentsAsync
  * Moved some MarketData methods to BlockTrading section: GetBlockTickersAsync, GetBlockTickerAsync, GetBlockTradesAsync
  * Removed some Funding methods: GetSavingBalancesAsync, SavingPurchaseRedemptionAsync
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/20](https://github.com/burakoner/OKX.Api/issues/20)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/29](https://github.com/burakoner/OKX.Api/issues/29)
  * Fixed issue [https://github.com/burakoner/OKX.Api/issues/34](https://github.com/burakoner/OKX.Api/issues/34)

* Version 1.2.4 - 05 Aug 2023
  * Multiple subscription to index candle instrument name issue solved
    as described at [https://github.com/burakoner/OKX.Api/issues/30](https://github.com/burakoner/OKX.Api/issues/30) and solved at [https://github.com/burakoner/OKX.Api/pull/31](https://github.com/burakoner/OKX.Api/pull/31)

* Version 1.2.3 - 03 Aug 2023
  * ApiSharp version updated to 1.4.1

* Version 1.2.2 - 28 Jul 2023
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/28](https://github.com/burakoner/OKX.Api/pull/28)

* Version 1.2.1 - 28 Jul 2023
  * Synced with OKX Api 2023-07-26 version
  * Added some other missing documentation symbols
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/25](https://github.com/burakoner/OKX.Api/pull/25)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/26](https://github.com/burakoner/OKX.Api/pull/26)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/27](https://github.com/burakoner/OKX.Api/pull/27)

* Version 1.2.0 - 27 Jul 2023
  * Added documentation symbols
  * Synced with OKX Api 2023-06-28 version
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/21](https://github.com/burakoner/OKX.Api/issues/21)
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/21](https://github.com/burakoner/OKX.Api/issues/21)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/23](https://github.com/burakoner/OKX.Api/pull/23)
  * Merged pull request [https://github.com/burakoner/OKX.Api/pull/24](https://github.com/burakoner/OKX.Api/pull/24)

* Version 1.1.7 - 26 Jun 2023
  * It's possible to subscribe multiple symbols at once on WebSocket
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/16](https://github.com/burakoner/OKX.Api/issues/16)

* Version 1.1.6 - 26 Jun 2023
  * Updated All Methods and Models, synced with OKX Api 2023-06-20 version
  * OKXStreamClient has some parameter and parameter order changes
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/18](https://github.com/burakoner/OKX.Api/issues/18)
  * Fixed some typos

* Version 1.1.5 - 25 Jun 2023
  * Added Grid Trading section endpoints
  * ApiSharp updated to v1.3.6
  * Fixed issue at [https://github.com/burakoner/OKX.Api/issues/11](https://github.com/burakoner/OKX.Api/issues/11)

* Version 1.1.0 - 07 May 2023
  * Updated All Methods and Models, synced with OKX Api 2023-04-27 version

* Version 1.0.6 - 06 May 2023
  * Updated WithdrawAsync Method [https://github.com/burakoner/OKEx.Net/issues/97](https://github.com/burakoner/OKEx.Net/issues/97)
  * Updated GetInstrumentsAsync Method [https://github.com/burakoner/OKX.Api/issues/7](https://github.com/burakoner/OKX.Api/issues/7)

* Version 1.0.0 - 25 Mar 2023
  * First Release
