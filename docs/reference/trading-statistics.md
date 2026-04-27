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

## Request-Model Examples

```csharp
var instrumentRange = new OkxRubikInstrumentPeriodRangeRequest
{
    InstrumentId = "BTC-USDT-SWAP",
    Period = "1D",
    Limit = 100
};

var contractOpenInterest = await api.Rubik.GetContractOpenInterestHistoryAsync(instrumentRange);
var topTraderAccountRatio = await api.Rubik.GetTopTradersContractLongShortRatioAsync(instrumentRange);
var topTraderPositionRatio = await api.Rubik.GetTopTradersContractLongShortRatioByPositionAsync(instrumentRange);
var contractLongShortRatio = await api.Rubik.GetContractLongShortRatioAsync(instrumentRange);

var takerVolume = await api.Rubik.GetTakerVolumeAsync(new OkxRubikTakerVolumeRequest
{
    Currency = "BTC",
    InstrumentType = OkxInstrumentType.Spot,
    Period = "1D"
});

var contractTakerVolume = await api.Rubik.GetContractTakerVolumeAsync(new OkxRubikContractTakerVolumeRequest
{
    InstrumentId = "BTC-USDT-SWAP",
    Period = "1D",
    Unit = "contract"
});

var currencyRange = new OkxRubikCurrencyPeriodRangeRequest
{
    Currency = "BTC",
    Period = "1D"
};

var marginLendingRatio = await api.Rubik.GetMarginLendingRatioAsync(currencyRange);
var longShortRatio = await api.Rubik.GetLongShortRatioAsync(currencyRange);
var contractSummary = await api.Rubik.GetContractSummaryAsync(currencyRange);

var optionPeriod = new OkxRubikOptionPeriodRequest
{
    Currency = "BTC-USD",
    Period = "1D"
};

var optionsSummary = await api.Rubik.GetOptionsSummaryAsync(optionPeriod);
var putCallRatio = await api.Rubik.GetPutCallRatioAsync(optionPeriod);
var interestVolumeExpiry = await api.Rubik.GetInterestVolumeExpiryAsync(optionPeriod);
var takerFlow = await api.Rubik.GetTakerFlowAsync(optionPeriod);

var interestVolumeStrike = await api.Rubik.GetInterestVolumeStrikeAsync(new OkxRubikOptionStrikeRequest
{
    Currency = "BTC-USD",
    ExpiryTime = "20240628",
    Period = "1D"
});
```

## Tips

- These endpoints are excellent for dashboards, signal generation, and analytics jobs.
- The wrapper exposes expiry statistics as date-based values instead of pretending they are Unix timestamps.


