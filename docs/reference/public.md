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

## Request-Model Overloads

The following `Public` methods have typed request-model overloads in addition to the shorter positional signatures:

- `GetCandlesticksAsync(OkxPublicCandlestickRequest)`
- `GetCandlestickHistoryAsync(OkxPublicCandlestickRequest)`
- `GetTradeHistoryAsync(OkxPublicTradeHistoryRequest)`
- `GetInstrumentsAsync(OkxPublicInstrumentQueryRequest)`
- `GetEventContractEventsAsync(OkxPublicEventContractEventsRequest)`
- `GetEventContractMarketsAsync(OkxPublicEventContractMarketsRequest)`
- `GetDeliveryExerciseHistoryAsync(OkxPublicDeliveryExerciseHistoryRequest)`
- `GetSettlementHistoryAsync(OkxPublicSettlementHistoryRequest)`
- `GetFundingRateHistoryAsync(OkxPublicFundingRateHistoryRequest)`
- `GetOpenInterestsAsync(OkxPublicOpenInterestRequest)`
- `GetPositionTiersAsync(OkxPublicPositionTierRequest)`
- `GetInsuranceFundsAsync(OkxPublicInsuranceFundQueryRequest)`
- `GetUnitConvertAsync(OkxPublicUnitConvertRequest)`
- `GetIndexCandlesticksAsync(OkxPublicCandlestickRequest)`
- `GetIndexCandlesticksHistoryAsync(OkxPublicCandlestickRequest)`
- `GetMarkPriceCandlesticksAsync(OkxPublicCandlestickRequest)`
- `GetMarkPriceCandlesticksHistoryAsync(OkxPublicCandlestickRequest)`
- `GetEconomicCalendarDataAsync(OkxPublicEconomicCalendarRequest)`
- `GetMarketDataHistoryAsync(OkxPublicMarketDataHistoryQueryRequest)`

## Request-Model Examples

```csharp
var candleRequest = new OkxPublicCandlestickRequest
{
    InstrumentId = "BTC-USDT",
    Period = "1H",
    Limit = 100
};

var candles = await api.Public.GetCandlesticksAsync(candleRequest);
var candleHistory = await api.Public.GetCandlestickHistoryAsync(candleRequest);
var indexCandles = await api.Public.GetIndexCandlesticksAsync(candleRequest);
var markPriceCandles = await api.Public.GetMarkPriceCandlesticksAsync(candleRequest);

var tradeHistory = await api.Public.GetTradeHistoryAsync(new OkxPublicTradeHistoryRequest
{
    InstrumentId = "BTC-USDT",
    Limit = 100
});

var instruments = await api.Public.GetInstrumentsAsync(new OkxPublicInstrumentQueryRequest
{
    InstrumentType = OkxInstrumentType.Swap,
    InstrumentFamily = "BTC-USDT"
});

var eventRequest = new OkxPublicEventContractEventsRequest
{
    SeriesId = "series-id",
    Limit = 20
};

var events = await api.Public.GetEventContractEventsAsync(eventRequest);
var markets = await api.Public.GetEventContractMarketsAsync(new OkxPublicEventContractMarketsRequest
{
    SeriesId = "series-id",
    EventId = "event-id",
    Limit = 20
});

var deliveryHistory = await api.Public.GetDeliveryExerciseHistoryAsync(new OkxPublicDeliveryExerciseHistoryRequest
{
    InstrumentType = OkxInstrumentType.Futures,
    InstrumentFamily = "BTC-USD",
    Limit = 20
});

var settlementHistory = await api.Public.GetSettlementHistoryAsync(new OkxPublicSettlementHistoryRequest
{
    InstrumentFamily = "BTC-USD",
    Limit = 20
});

var fundingRateHistory = await api.Public.GetFundingRateHistoryAsync(new OkxPublicFundingRateHistoryRequest
{
    InstrumentId = "BTC-USD-SWAP",
    Limit = 20
});

var openInterests = await api.Public.GetOpenInterestsAsync(new OkxPublicOpenInterestRequest
{
    InstrumentType = OkxInstrumentType.Swap,
    InstrumentFamily = "BTC-USDT"
});

var positionTiers = await api.Public.GetPositionTiersAsync(new OkxPublicPositionTierRequest
{
    InstrumentType = OkxInstrumentType.Futures,
    MarginMode = OkxAccountMarginMode.Isolated,
    InstrumentFamily = "BTC-USD"
});

var insuranceFunds = await api.Public.GetInsuranceFundsAsync(new OkxPublicInsuranceFundQueryRequest
{
    InstrumentType = OkxInstrumentType.Margin,
    Currency = "BTC",
    Limit = 20
});

var unitConvert = await api.Public.GetUnitConvertAsync(new OkxPublicUnitConvertRequest
{
    InstrumentId = "BTC-USDT-SWAP",
    Size = 100,
    Price = 65000m,
    Type = OkxPublicConvertType.CurrencyToContract,
    Unit = OkxPublicConvertUnit.Coin,
    OperationType = OkxPublicConvertOperation.Open
});

var calendar = await api.Public.GetEconomicCalendarDataAsync(new OkxPublicEconomicCalendarRequest
{
    Region = "US",
    Limit = 20
});

var marketDataHistory = await api.Public.GetMarketDataHistoryAsync(new OkxPublicMarketDataHistoryQueryRequest
{
    Module = OkxPublicMarketDataHistoryModule.BorrowingRate,
    InstrumentType = OkxInstrumentType.Margin,
    DateAggregationType = OkxPublicDateAggregationType.Daily,
    InstrumentIdList = "BTC-USDT"
});
```

## Tips

- For public trading dashboards, `api.Public` and `api.Rubik` are usually the two most important read-only clients.
- Use typed request overloads when you need many optional filters or when you want future additions to be easier to absorb.
- Event-contract endpoints may require signed access depending on the route and OKX environment.


