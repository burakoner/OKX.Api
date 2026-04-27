# Trading Statistics (Rubik)

[Docs Home](../index.md) | [REST Reference](./index.md) | [Public](./public.md) | [Funding](./funding.md)

Official OKX docs: [Trading Statistics](https://www.okx.com/docs-v5/en/#trading-statistics)

## Overview

`api.Rubik` exposes OKX statistical and analytics endpoints. `api.TradingStatistics` is an alias for the same client.

These methods are public and unsigned.

## Example Calls

```csharp
var coins = await api.Rubik.GetSupportCoinAsync();
var openInterest = await api.Rubik.GetContractOpenInterestHistoryAsync("BTC", OkxPeriod.FiveMinutes);
var takerVolume = await api.Rubik.GetTakerVolumeAsync(OkxRubikTradeType.Spot, OkxPeriod.OneDay);
var longShort = await api.Rubik.GetContractLongShortRatioAsync("BTC", OkxPeriod.OneDay);
var putCall = await api.Rubik.GetPutCallRatioAsync("BTC-USD", OkxPeriod.OneDay);
```

## Method Catalog

### Discovery

- `GetSupportCoinAsync`

### Open Interest and Volume

- `GetContractOpenInterestHistoryAsync`
- `GetContractSummaryAsync`
- `GetOptionsSummaryAsync`
- `GetInterestVolumeExpiryAsync`
- `GetInterestVolumeStrikeAsync`

### Ratios

- `GetMarginLendingRatioAsync`
- `GetTopTradersContractLongShortRatioAsync`
- `GetTopTradersContractLongShortRatioByPositionAsync`
- `GetContractLongShortRatioAsync`
- `GetLongShortRatioAsync`
- `GetPutCallRatioAsync`

### Taker Flow and Volume

- `GetTakerVolumeAsync`
- `GetContractTakerVolumeAsync`
- `GetTakerFlowAsync`

## Request-Model Overloads

The following `Rubik` / `TradingStatistics` methods have typed request-model overloads in addition to the shorter positional signatures:

- `GetContractOpenInterestHistoryAsync(OkxRubikInstrumentPeriodRangeRequest)`
- `GetTakerVolumeAsync(OkxRubikTakerVolumeRequest)`
- `GetContractTakerVolumeAsync(OkxRubikContractTakerVolumeRequest)`
- `GetMarginLendingRatioAsync(OkxRubikCurrencyPeriodRangeRequest)`
- `GetTopTradersContractLongShortRatioAsync(OkxRubikInstrumentPeriodRangeRequest)`
- `GetTopTradersContractLongShortRatioByPositionAsync(OkxRubikInstrumentPeriodRangeRequest)`
- `GetContractLongShortRatioAsync(OkxRubikInstrumentPeriodRangeRequest)`
- `GetLongShortRatioAsync(OkxRubikCurrencyPeriodRangeRequest)`
- `GetContractSummaryAsync(OkxRubikCurrencyPeriodRangeRequest)`
- `GetOptionsSummaryAsync(OkxRubikOptionPeriodRequest)`
- `GetPutCallRatioAsync(OkxRubikOptionPeriodRequest)`
- `GetInterestVolumeExpiryAsync(OkxRubikOptionPeriodRequest)`
- `GetInterestVolumeStrikeAsync(OkxRubikOptionStrikeRequest)`
- `GetTakerFlowAsync(OkxRubikOptionPeriodRequest)`

## Tips

- These endpoints are excellent for dashboards, signal generation, and analytics jobs.
- The wrapper exposes expiry statistics as date-based values instead of pretending they are Unix timestamps.


