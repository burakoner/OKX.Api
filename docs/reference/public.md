# Public and Market Data

[Docs Home](../index.md) | [REST Reference](./index.md) | [Trade](./trade.md) | [Trading Statistics](./trading-statistics.md)

Official OKX docs: [Public Data](https://www.okx.com/docs-v5/en/#public-data)

## Overview

`api.Public` is the largest read-heavy section in the library. It covers:

- market data such as tickers, order books, trades, and candles
- public reference data such as instruments, position tiers, insurance funds, and loan quota
- event contracts
- announcements and system upgrade status

`api.Market` is an alias for `api.Public`.

Most methods are public and unsigned. A few flows can optionally be signed when OKX expects authenticated public access.

## Example Calls

```csharp
var tickers = await api.Public.GetTickersAsync(OkxInstrumentType.Spot);
var ticker = await api.Public.GetTickerAsync("BTC-USDT");
var book = await api.Public.GetOrderBookAsync("BTC-USDT", 40);
var candles = await api.Public.GetCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
var trades = await api.Public.GetTradesAsync("BTC-USDT");
```

Reference-data examples:

```csharp
var instruments = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Spot);
var fundingRates = await api.Public.GetFundingRatesAsync("BTC-USD-SWAP");
var positionTiers = await api.Public.GetPositionTiersAsync(
    OkxInstrumentType.Futures,
    OkxAccountMarginMode.Isolated,
    "BTC-USD");
var loanQuota = await api.Public.GetInterestRateLoanQuotaAsync();
var insuranceFunds = await api.Public.GetInsuranceFundsAsync(OkxInstrumentType.Margin, currency: "BTC");
var serverTime = await api.Public.GetServerTimeAsync();
```

Event-contract examples:

```csharp
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var series = await api.Public.GetEventContractSeriesAsync();
var events = await api.Public.GetEventContractEventsAsync("series-id");
var markets = await api.Public.GetEventContractMarketsAsync("series-id", eventId: "event-id");
```

## Method Catalog

Overload note: candle, trade-history, instruments, delivery/settlement history, insurance fund, unit convert, economic calendar, and market-data-history methods also have typed request-model overloads.

### Market Data

- `GetTickersAsync`
- `GetTickerAsync`
- `GetOrderBookAsync`
- `GetOrderBookFullAsync`
- `GetCandlesticksAsync`
- `GetCandlestickHistoryAsync`
- `GetTradesAsync`
- `GetTradeHistoryAsync`
- `GetOptionTradesByInstrumentFamilyAsync`
- `GetOptionTradesAsync`
- `Get24HourVolumeAsync`
- `GetCallAuctionDetailsAsync`

### Instruments and Event Contracts

- `GetInstrumentsAsync`
- `GetEventContractSeriesAsync`
- `GetEventContractEventsAsync`
- `GetEventContractMarketsAsync`

### Pricing, Delivery, Settlement, and Funding

- `GetEstimatedPriceAsync`
- `GetDeliveryExerciseHistoryAsync`
- `GetEstimatedSettlementInfoAsync`
- `GetSettlementHistoryAsync`
- `GetFundingRatesAsync`
- `GetFundingRateHistoryAsync`
- `GetOpenInterestsAsync`
- `GetLimitPriceAsync`
- `GetOptionMarketDataAsync`
- `GetDiscountInfoAsync`
- `GetMarkPricesAsync`
- `GetPremiumHistoryAsync`

### Risk, Reference, and Utility Data

- `GetServerTimeAsync`
- `GetPositionTiersAsync`
- `GetInterestRatesAsync`
- `GetInterestRateLoanQuotaAsync`
- `GetUnderlyingAsync`
- `GetInsuranceFundAsync`
- `GetInsuranceFundsAsync`
- `GetUnitConvertAsync`
- `GetOptionTickBandsAsync`
- `GetExchangeRateAsync`
- `GetIndexComponentsAsync`
- `GetEconomicCalendarDataAsync`
- `GetMarketDataHistoryAsync`

### Index and Mark Price Candles

- `GetIndexTickersAsync`
- `GetIndexCandlesticksAsync`
- `GetIndexCandlesticksHistoryAsync`
- `GetMarkPriceCandlesticksAsync`
- `GetMarkPriceCandlesticksHistoryAsync`

### Announcements and Status

- `GetSystemUpgradeStatusAsync`
- `GetAnnouncementsAsync`
- `GetAnnouncementTypesAsync`

## Tips

- For public trading dashboards, `api.Public` and `api.Rubik` are usually the two most important read-only clients.
- Use typed request overloads when you need many optional filters or when you want future additions to be easier to absorb.
- Event-contract endpoints may require signed access depending on the route and OKX environment.


