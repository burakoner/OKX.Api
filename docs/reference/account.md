# Account

[Docs Home](../index.md) | [REST Reference](./index.md) | [Trade](./trade.md) | [Funding](./funding.md)

Official OKX docs: [Trading Account](https://www.okx.com/docs-v5/en/#trading-account)

## Overview

`api.Account` covers trading-account state: balances, positions, leverage, fees, bills, margin settings, risk controls, and portfolio tools.

Use it through:

```csharp
var api = new OkxRestApiClient();
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");
```

All `Account` methods are private and require credentials.

## Example Calls

```csharp
var instruments = await api.Account.GetInstrumentsAsync(OkxInstrumentType.Spot);
var balances = await api.Account.GetBalancesAsync();
var positions = await api.Account.GetPositionsAsync();
var positionHistory = await api.Account.GetPositionsHistoryAsync();
var bills = await api.Account.GetBillHistoryAsync();
var config = await api.Account.GetConfigurationAsync();
var leverage = await api.Account.GetLeverageAsync("BTC-USD-240628", OkxAccountMarginMode.Isolated);
var feeRates = await api.Account.GetFeeRatesAsync(OkxInstrumentType.Spot);
var greeks = await api.Account.GetGreeksAsync();
var mmp = await api.Account.GetMmpAsync("BTC-USDT");
var glpToday = await api.Account.GetGlpTodayPerformanceAsync();
var glpHistory = await api.Account.GetGlpHistoricalPerformanceAsync(OkxAccountGlpProgram.Spot);
```

Example configuration flow:

```csharp
await api.Account.SetPositionModeAsync(OkxTradePositionMode.LongShortMode);
await api.Account.SetLeverageAsync(5, instrumentId: "BTC-USDT-SWAP", marginMode: OkxAccountMarginMode.Cross);
await api.Account.SetGreeksAsync(OkxAccountGreeksType.GreeksInCoins);
await api.Account.SetTradingConfigAsync(OkxAccountStrategyType.DeltaNeutral);
```

Demo balance adjustment requires a demo client and changes the demo account balance. The operation is atomic. Increase requests are limited to three per user per UTC day; reduce requests have no request-count limit. A single increase request may add at most 1 BTC, 1 ETH, 5,000 USDT, and 100 OKB:

```csharp
var demoApi = new OkxRestApiClient(new OkxRestApiOptions
{
    DemoTradingService = true
});
demoApi.SetApiCredentials("DEMO-API-KEY", "DEMO-API-SECRET", "DEMO-API-PASSPHRASE");

var adjustment = await demoApi.Account.AdjustDemoAccountBalanceAsync(
    new OkxAccountDemoBalanceAdjustmentRequest
    {
        Type = OkxAccountDemoBalanceAdjustmentType.Increase,
        Adjustments =
        [
            new()
            {
                Currency = "USDT",
                Amount = 100m
            }
        ]
    });
```

## Method Catalog

### Bills, Balances, and Positions

- `GetBillTypesAsync`
- `GetInstrumentsAsync`
- `GetBalancesAsync`
- `GetPositionsAsync`
- `GetPositionsHistoryAsync`
- `GetPositionRiskAsync`
- `GetBillHistoryAsync`
- `GetBillArchiveAsync`
- `ApplyBillDataAsync`
- `GetBillDataAsync`

`ApplyBillDataAsync` applies for unified-account bill data outside the current quarter; OKX limits it to one request per 10 seconds per user ID. `GetBillDataAsync` checks the generated link and has a separate OKX limit of 10 requests per 2 seconds. The client mirrors both limits within each client instance. Both methods require a four-digit year and a valid quarter, and accept multiple bill types which are sent as one comma-separated `type` value. Use `GetBillTypesAsync` for the runtime type/subtype mapping.

When `ApplyBillDataAsync` returns `false`, OKX says to check the link after two hours; generation may take longer at peak load and support should be contacted if it is still unavailable after three hours. A generated download link expires after 5.5 hours, and OKX says the same quarter does not need to be applied for again within 30 days.

### Configuration and Leverage

- `ActivateFeatureAsync`
- `GetConfigurationAsync`
- `SetPositionModeAsync`
- `SetLeverageAsync`
- `GetLeverageAsync`
- `GetLeverageEstimatedInformationAsync`
- `SwitchPresetAccountModeAsync`
- `PrecheckAccountModeSwitchAsync`
- `SetTradingConfigAsync`
- `PrecheckSetDeltaNeutralAsync`
- `SetLevelAsync`

### Explicit USDC Feature Activation

`ActivateFeatureAsync(OkxAccountFeature.UsdcOrderBookTrading)` implements signed `POST /api/v5/account/activate-feature` with the required string `feature: "1"`. The complete [official contract](https://www.okx.com/docs-v5/en/#trading-account-rest-api-activate-feature) says to call it **only after order placement returns 54109**, not proactively for every account or instrument. It changes account state and is never called automatically by Trade methods.

Activation is shared by the master account and all its sub-accounts; the master or any one sub-account only needs to call once. Coordinate that choice across your application/accounts. No local activation cache, capability inference, order replay, or retry is introduced. Error `51773` means this activation feature is unsupported (the general error table describes regional availability); it does **not** prove Crypto-USDC trading is unavailable. Do not send a real order merely to probe that question.

Success contains `data: []` and no response parameters, so the method returns native dataless `RestCallResult`, preserving `Success`, numeric errors/messages and HTTP request/response metadata, without inventing a boolean or timestamp response. The SDK's dataless result has no `Raw` property. Missing credentials retain the existing client authentication exception convention; cancellation is forwarded normally.

The registered endpoint guard allows 5 requests per 2 seconds within the configured client limiter. The official key is User ID, so applications using multiple keys/clients/processes for the same account must coordinate their combined usage; this is not a global account limiter. See [Crypto-USDC migration](./trade.md#crypto-usd-to-crypto-usdc-migration) for account quote-currency selection.

### Limits, Fees, and Rates

- `GetMaximumOrderQuantityAsync`
- `GetMaximumAvailableAmountAsync`
- `GetMaximumLoanAmountAsync`
- `GetFeeRatesAsync`
- `GetInterestAccruedAsync`
- `GetInterestRateAsync`
- `GetInterestLimitsAsync`
- `GetMaximumWithdrawalsAsync`
- `GetRiskStateAsync`

### Global Liquidity Program

- `GetGlpTodayPerformanceAsync`
- `GetGlpHistoricalPerformanceAsync`

These signed, read-only endpoints are available only to enrolled GLP accounts; when called with a sub-account API key, OKX resolves the master account. The client applies the documented, separate limit of five requests per two seconds to each endpoint.

`GetGlpTodayPerformanceAsync` returns daily and month-to-date volume/share metrics for all enrolled programs. `DataDate` is a UTC+8 `yyyy-MM-dd` snapshot date, normally T-1 and falling back to T-2 while T-1 is incomplete. When `DataReady` is false, `Programs` is empty. `FUT_NTO` maps to `OkxAccountGlpProgram.ExpiryAndNitro`; its Type A, Type B, and TradFi category objects can be null while `Total` remains present.

`GetGlpHistoricalPerformanceAsync` requires a program and returns newest-first daily rows. `begin` and `end` are inclusive Unix-millisecond filters interpreted by OKX as UTC+8 dates. Defaults are the first day of the current month and today; the default page size is 31 and the maximum is 100.

Official contracts: [today performance](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-get-glp-today-performance) and [historical performance](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-get-glp-historical-performance).

### Margin, Borrow, and Collateral

- `SetMarginBalanceAsync`
- `ManualBorrowRepayAsync`
- `ManualBorrowAsync`
- `ManualRepayAsync`
- `SetAutoRepayAsync`
- `GetBorrowRepayHistoryAsync`
- `SetAutoLoanAsync`
- `SetCollateralAssetsAsync`
- `GetCollateralAssetsAsync`
- `SetSettleCurrencyAsync`

### Options, MMP, and Portfolio Tools

- `SetFeeTypeAsync`
- `SetGreeksAsync`
- `SetIsolatedMarginModeAsync`
- `PositionBuilderAsync`
- `SetRiskOffsetAmountAsync`
- `GetGreeksAsync`
- `GetPositionTiersAsync`
- `ActivateOptionAsync`
- `ResetMmpAsync`
- `SetMmpAsync`
- `GetMmpAsync`
- `MovePositionsAsync`
- `SetAutoEarnAsync`

### Demo Trading

- `AdjustDemoAccountBalanceAsync`

## Request-Model Overloads

The following `Account` methods have typed request-model overloads in addition to the shorter positional signatures:

- `GetPositionsHistoryAsync(OkxAccountPositionsHistoryRequest)`
- `GetBillHistoryAsync(OkxAccountBillQueryRequest)`
- `GetBillArchiveAsync(OkxAccountBillQueryRequest)`
- `SetLeverageAsync(OkxAccountSetLeverageRequest)`
- `GetMaximumLoanAmountAsync(OkxAccountMaximumLoanAmountRequest)`
- `GetInterestAccruedAsync(OkxAccountInterestAccruedRequest)`
- `GetBorrowRepayHistoryAsync(OkxAccountBorrowRepayHistoryRequest)`

## Request-Model Examples

```csharp
var positionHistoryRequest = new OkxAccountPositionsHistoryRequest
{
    InstrumentType = OkxInstrumentType.Swap,
    InstrumentId = "BTC-USDT-SWAP",
    MarginMode = OkxAccountMarginMode.Cross,
    Limit = 50
};

var positionHistory = await api.Account.GetPositionsHistoryAsync(positionHistoryRequest);

var billQuery = new OkxAccountBillQueryRequest
{
    Currency = "USDT",
    Begin = DateTimeOffset.UtcNow.AddDays(-7).ToUnixTimeMilliseconds(),
    Limit = 100
};

var billHistory = await api.Account.GetBillHistoryAsync(billQuery);
var billArchive = await api.Account.GetBillArchiveAsync(billQuery);

var leverageRequest = new OkxAccountSetLeverageRequest
{
    Leverage = 3,
    InstrumentId = "BTC-USDT-SWAP",
    MarginMode = OkxAccountMarginMode.Cross
};

await api.Account.SetLeverageAsync(leverageRequest);

var maximumLoan = await api.Account.GetMaximumLoanAmountAsync(new OkxAccountMaximumLoanAmountRequest
{
    MarginMode = OkxAccountMarginMode.Cross,
    InstrumentId = "BTC-USDT",
    MarginCurrency = "USDT"
});

var interestAccrued = await api.Account.GetInterestAccruedAsync(new OkxAccountInterestAccruedRequest
{
    Currency = "USDT",
    Limit = 20
});

var borrowRepayHistory = await api.Account.GetBorrowRepayHistoryAsync(new OkxAccountBorrowRepayHistoryRequest
{
    Currency = "USDT",
    Limit = 20
});
```

## Tips

- Prefer request-model overloads when a call takes many optional filters.
- `GetFeeRatesAsync` accepts `instId` only for SPOT/MARGIN and `instFamily` only for FUTURES/SWAP/OPTION; `groupId` cannot be combined with either selector. `feeGroup[].RpiMaker` is the current RPI rate, while `ElpMaker` preserves OKX's temporary alias and `EffectiveRpiMaker` reads either form. The deprecated root fee fields remain available for payload compatibility. OKX limits this endpoint to 5 requests per 2 seconds per User ID.
- Maker/taker fee-rate signs follow OKX's contract: positive means rebate and negative means commission. Delivery and exercise rates use positive values for commission. Zero-fee trading is not reflected by this endpoint.
- `GetPositionTiersAsync` returns all rows that OKX sends; do not assume a single result.
- `PositionBuilderAsync` is useful for portfolio margin and delta-neutral tooling, not day-to-day spot usage.
- Private `GetInstrumentsAsync` requires `seriesId` for EVENTS and `instFamily` for OPTION. A `FUTURES` query also returns Pre-market X-Perps as `RuleType = PreMarket`, changing to `XPerp` after conversion. SWAP/FUTURES responses expose both USD `maxPlatOILmt` and coin-denominated `maxPlatOICoinLmt`; OKX reports platform-limit opening rejections with error `54031`.
- Private instruments may contain both the current `RpiMakerPermission` field and the deprecated `ElpMakerPermission` alias through October 31, 2026. Use `EffectiveRpiMakerPermission`: it gives `rpi` precedence when both are present and falls back to `elp` for transitional responses.
- For SPOT, private `TradeQuoteCurrencyList` supplies the quote currencies allowed for the account; the public quote symbol alone is not an account permission. Instruments request/schema parity and the retained conservative per-type-throttling boundary are documented in the [catalog reference](./public.md#instrument-catalog-and-incremental-updates).
- `MovePositionsAsync` requires a VIP6 master-account API key, different source and destination accounts under the same master account, a 1-to-32 character alphanumeric client ID, and at most 30 legs. Margin trading positions are unsupported; the current official contract supports TradeFi positions, including equity perpetuals/XPerp.
- `AdjustDemoAccountBalanceAsync` is rejected locally unless `DemoTradingService` is enabled. It supports only BTC, ETH, USDT, and OKB; OKX validates the current precision for each currency server-side.
- OKX reports exhausted daily increase quota as `59691`, insufficient balance as `59692`, and insufficient transferable balance as `59693`.
- GLP endpoints return `50030` when the API key lacks access. Required/invalid or mismatched filters remain available through the standard OKX errors `50014`, `51000`, and `50016`.


