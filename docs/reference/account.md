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
```

Example configuration flow:

```csharp
await api.Account.SetPositionModeAsync(OkxTradePositionMode.LongShortMode);
await api.Account.SetLeverageAsync(5, instrumentId: "BTC-USDT-SWAP", marginMode: OkxAccountMarginMode.Cross);
await api.Account.SetGreeksAsync(OkxAccountGreeksType.GreeksInCoins);
await api.Account.SetTradingConfigAsync(OkxAccountStrategyType.DeltaNeutral);
```

## Method Catalog

Overload note: long filter-heavy methods such as bills, positions history, loan queries, and account reports also support typed request-model overloads.

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

### Configuration and Leverage

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

## Tips

- Prefer request-model overloads when a call takes many optional filters.
- `GetPositionTiersAsync` returns all rows that OKX sends; do not assume a single result.
- `PositionBuilderAsync` is useful for portfolio margin and delta-neutral tooling, not day-to-day spot usage.


