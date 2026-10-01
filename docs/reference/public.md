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
var rpiBook = await api.Public.GetRpiOrderBookAsync("BTC-USDT-SWAP", 40);
var candles = await api.Public.GetCandlesticksAsync("BTC-USDT", OkxPeriod.OneHour);
var trades = await api.Public.GetTradesAsync("BTC-USDT");
```

Reference-data examples:

```csharp
var instruments = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Spot);
var marketMakerPairs = await api.Public.GetMarketMakerInstrumentTypesAsync(OkxInstrumentType.Swap);
var deltaHedgeCurrencies = await api.Public.GetDeltaHedgeCurrenciesAsync();
var ethDeltaHedges = await api.Public.GetDeltaHedgeCurrenciesAsync("ETH");
var fundingRates = await api.Public.GetFundingRatesAsync("BTC-USD-SWAP");
var positionTiers = await api.Public.GetPositionTiersAsync(
    OkxInstrumentType.Futures,
    OkxAccountMarginMode.Isolated,
    "BTC-USD");
var loanQuota = await api.Public.GetInterestRateLoanQuotaAsync();
var insuranceFunds = await api.Public.GetInsuranceFundsAsync(OkxInstrumentType.Margin, currency: "BTC");
var serverTime = await api.Public.GetServerTimeAsync();
```

### Delta Hedge Currency Mappings

`GetDeltaHedgeCurrenciesAsync` implements the complete current [Delta hedge currencies endpoint](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-delta-hedge-currencies), identified by the [August 26 entry](https://www.okx.com/docs-v5/log_en/#2026-08-26). It sends an unsigned `GET /api/v5/public/delta-hedge-currencies` without requiring API credentials. Omit `currency` for the full mapping; supply it to send `ccy` and request only that currency's mapping entry. An explicitly empty/whitespace filter is rejected locally; currency names are otherwise forwarded without a hardcoded catalog or rewriting.

The result is a list of `OkxPublicDeltaHedgeCurrency`: `Currency` maps to `ccy`, and `HedgeCurrencies` preserves the `hedgeCcy` string array. The official example includes ETH/BETH, XAU/XAUT, and AAPL/XAAPL. OKX describes the underlying-asset relationship as symmetric; the wrapper preserves the server's entries and array order without synthesizing reverse entries, assuming one hedge per currency, or expanding a filtered response into a full mapping. An empty `data` array remains an empty successful result.

The endpoint has its own 20-requests-per-2-seconds IP budget, shared across filtered/unfiltered calls on the client. The registered local guard is not a cross-process/global IP coordinator; applications sharing an outbound IP must coordinate total usage. Existing rate-limiting configuration controls waiting versus failure, and the method forwards the cancellation token and server errors without adding retries or caching.

This is reference data, not a hedge sizing calculation, price/conversion guarantee, account permission, or automatic delta-neutral strategy activation. The 16 deterministic tests cover the official example and synthetic edge/error responses through a local server; they do not verify live mappings or production hedge/rate-limit enforcement.

### Instrument Catalog and Incremental Updates

The [September 30 notice](https://www.okx.com/docs-v5/log_en/#2026-09-30) changes the list-level assumption: the [instruments channel](https://www.okx.com/docs-v5/en/#public-data-websocket-instruments-channel) can send only changed instruments, and further scenarios may become incremental without another announcement. Never replace your complete catalog with a push's `data` array or delete instruments merely because they are absent. There is no guaranteed initial full list, snapshot/delta flag, or sequence field in this channel's current contract.

`SubscribeToInstrumentsAsync` already forwards each actual record without a wrapper-owned catalog. Upsert application entries by `InstrumentId`, preserve unrelated records, and treat an explicit `Expired` record as lifecycle information. Multiple records and repeated records are forwarded; an empty data array produces no callbacks. `UpcomingChanges` describes future `tickSz`, `minSz`, or `maxMktSz` changes, not values to apply before their effective time.

Use [Public Get instruments](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-instruments) for a catalog refresh and [Account Get instruments](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-instruments) for account-specific metadata, especially `TradeQuoteCurrencyList`. Coordinate bootstrap, buffered pushes, reconnect refreshes, and concurrent catalog access in the application; the wrapper does not promise an atomic REST/WS snapshot or gap-free reconciliation. Announcement-time records can contain empty trading fields and an unavailable code; do not fill them from guessed/stale metadata and treat them as ready to trade.

Documentation caveat: the channel's older canceled-preopen-listing paragraph still describes a full list excluding the canceled symbol. That does not establish safe absence-as-deletion for all pushes under the newer incremental notice. Deliberate REST reconciliation is needed for removals not explicitly reported; no automatic removal policy is introduced.

The official REST budgets are 20 requests per 2 seconds by **IP + instType** (public) and **User ID + instType** (account). Existing default throttling remains a conservative, combined 20-per-2-second budget across these and other non-exempt requests, not separate per-type budgets or cross-client/IP/User-ID coordination. ApiSharp 4.5.1 passes only the URI path to its configured rate limiters, not `instType`; see the [September 30 boundary](../maintenance-plan.md#september-30--2026-10-01). No throughput increase or exact per-type parity is claimed.

For the affected Crypto-USD SPOT instruments, refresh IDs **and** codes for the corresponding Crypto-USDC instruments. Unlike the specific SPCX rename below, do not assume the old code is preserved; no server-side legacy mapping is provided. Update subscriptions, request construction, parsing, and every ID/code cache deliberately; see [quote-currency migration](./trade.md#crypto-usd-to-crypto-usdc-migration).

### Pre-market X-Perp Instruments

Pre-market X-Perps are returned by the public and private instruments REST endpoints, and by the `instruments` WebSocket channel, with `InstrumentType = OkxInstrumentType.Futures`. Do not classify every `FUTURES` instrument as a conventional expiry future; inspect `RuleType` as well.

- During the pre-market phase, `RuleType` is `OkxInstrumentRuleType.PreMarket`.
- After conversion to a normal X-Perp, `RuleType` changes to `OkxInstrumentRuleType.XPerp`.
- `PreMarketSwitchTimestamp` and `PreMarketSwitchTime` are populated when a Pre-market X-Perp converts to a normal X-Perp.

All three surfaces deserialize to `OkxPublicInstrument`, so the same classification logic can be shared across REST catalog refreshes and WebSocket updates.

### Security Fund and ADL Warning Contract

Security fund queries require `Currency` for `MARGIN`; `FUTURES`, `SWAP`, and `OPTION` require `InstrumentFamily`. `Currency` and `InstrumentFamily` are mutually exclusive across those two query shapes. OKX removed `regular_update` from the request filter. The `platform_revenue` and `adl` filters remain available but are deprecated and currently return empty detail lists.

There is a confirmed documentation/production divergence: although the current documentation says the `regular_update` response type was removed, a read-only production check on 08 Aug 2026 still returned `regular_update` rows for an unfiltered/`all` request. The client therefore does not expose `regular_update` as a request filter but continues to deserialize it in responses to avoid data loss.

`SubscribeToAdlWarningsAsync` receives no pushes in the `normal` state. `warning` and `adl` pushes arrive once per second. The deprecated `ccy`, `maxBal`, `maxBalTs`, `adlType`, `adlBal`, `adlRecBal`, `decRate`, `adlRate`, and `adlRecRate` fields are retained as nullable compatibility properties because OKX still includes their keys with empty-string values.

Event-contract endpoints are public and do not require API credentials:

```csharp
var series = await api.Public.GetEventContractSeriesAsync();
var events = await api.Public.GetEventContractEventsAsync("series-id");
var markets = await api.Public.GetEventContractMarketsAsync("series-id", eventId: "event-id");
var eventTickBands = await api.Public.GetInstrumentTickBandsAsync(OkxInstrumentType.Events);
```

### WebSocket Order Book Integrity

The `books`, `books-l2-tbt`, and `books50-l2-tbt` channels still include `checksum`, but OKX now fixes it to `0`; it must not be used for integrity checks. `books5`, `bbo-tbt`, `books-elp`, and `books-rpi` do not use it. The compatibility `Checksum` property remains available but is obsolete.

For incremental books, compare each update's `PreviousSequenceId` with the last accepted `SequenceId` before applying the delta:

```csharp
var ws = new OkxWebSocketApiClient();
long? lastSequenceId = null;

await ws.Public.SubscribeToOrderBookAsync(book =>
{
    if (book.Action == "snapshot")
    {
        // Rebuild the local book from book.Asks and book.Bids.
        lastSequenceId = book.SequenceId;
        return;
    }

    if (lastSequenceId.HasValue && book.PreviousSequenceId != lastSequenceId)
    {
        Console.WriteLine("Order book gap detected; discard local state and resubscribe for a snapshot.");
        return;
    }

    // Apply the incremental asks and bids, then advance the sequence.
    lastSequenceId = book.SequenceId;
}, "BTC-USDT", OkxOrderBookType.OrderBook);
```

Do not reject an update merely because `SequenceId` equals or is lower than `PreviousSequenceId`: equal values are valid keepalive updates, and OKX may reset the sequence during maintenance. Continuity still depends on `PreviousSequenceId` matching the last accepted sequence.

`OrderBook_RPI` maps to the current `books-rpi` channel, which combines organic and RPI liquidity. For that channel, `Quantity` is total quantity and `NonRpiQuantity` is the organic-only portion. The legacy `LiquidatedOrders` name is obsolete because the third order-book value never represented liquidations. `OrderBook_ELP` remains as an obsolete compatibility value through OKX's 31 October 2026 sunset.

`GetRpiOrderBookAsync` exposes the matching REST `GET /api/v5/market/books-rpi` snapshot. It accepts 1-400 levels per side through the wire-level `sz` parameter, is limited to 20 requests per 2 seconds per IP, and returns the server's current `SequenceId`. OKX refreshes this server-side cache every 200 ms, so it is not an immediate matching-engine read. Each row uses the same `[price, totalQty, nonRpiQty, count]` shape as the WebSocket channel.

`GetTradesAsync` accepts up to 500 rows and is limited by OKX to 100 requests per 2 seconds per IP. `OkxPublicTrade.Source = RetailPriceImprovementOrder` represents wire value `1`; `EnhancedLiquidityProgramOrder` remains an obsolete source-compatible name during the transition.

### RPI Tradeable Depth and Visibility

The current [REST RPI book contract](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-get-rpi-order-book) includes organic depth plus **currently tradeable** RPI, not every resting RPI order. Hidden/non-tradeable RPI is excluded platform-side. The [WebSocket order-book contract](https://www.okx.com/docs-v5/en/#order-book-trading-market-data-ws-order-book-channel) describes `books-rpi` as a 400-level initial snapshot followed by 100 ms deltas, using `seqId`/`prevSeqId` without checksum. The wrapper forwards rows/deltas; it does not maintain or reconstruct the local book.

- On these consolidated feeds, `Quantity - NonRpiQuantity` is the tradeable RPI amount at that level. A taker with `rpiTakerAccess=true` can access total depth; without access, only `NonRpiQuantity` is executable. Permission alone does not guarantee available RPI liquidity.
- The REST endpoint explicitly fails closed when RPI tradeability state is unavailable: RPI is excluded and each row returns equal total/organic quantities. Keep valid organic depth; do not invent an RPI quantity or fall back to the deprecated raw `books-elp` feed.
- Equal quantities do not distinguish no RPI quoting from hidden RPI (or, on REST, unavailable tradeability state). There is no hidden-order/status field to infer these cases from the consolidated rows.
- The [August 11 behavior notice](https://www.okx.com/docs-v5/log_en/#2026-08-11) states that an RPI crossing the opposite organic best price is hidden; bid and ask RPI crossing each other inside the organic spread are both hidden. This detailed visibility rule supplements the endpoint tables; it is not a new payload field.

Do not use a cached REST snapshot or a local WebSocket book to promise that an RPI maker price will be accepted. [Placement and amendment spacing rules](./trade.md#rpi-maker-spacing-and-amendment) are evaluated by OKX's matching engine on arrival.

## Method Catalog

### Market Data

- `GetTickersAsync`
- `GetTickerAsync`
- `GetOrderBookAsync`
- `GetRpiOrderBookAsync`
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
- `GetMarketMakerInstrumentTypesAsync`
- `GetEventContractSeriesAsync`
- `GetEventContractEventsAsync`
- `GetEventContractMarketsAsync`

`GetMarketMakerInstrumentTypesAsync` is an unsigned, IP-limited public read for SPOT and SWAP instruments. It returns the current MM Program classifications `A`, `B-Crypto`, and the SWAP-only `B-TradFi`; an optional instrument ID narrows the response to at most one record.

The official response table currently documents those three values without a prefix. A read-only production check on 08 Aug 2026 returned `Type A`, `Type B-Crypto`, and `Type B-TradFi` instead. The client accepts both representations and maps them to the same enum values; the documented unprefixed value remains the canonical serialization label.

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

- `GetDeltaHedgeCurrenciesAsync`
- `GetServerTimeAsync`
- `GetPositionTiersAsync`
- `GetInterestRatesAsync`
- `GetInterestRateLoanQuotaAsync`
- `GetUnderlyingAsync`
- `GetInsuranceFundAsync`
- `GetInsuranceFundsAsync`
- `GetUnitConvertAsync`
- `GetInstrumentTickBandsAsync`
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

var historyEnd = DateTimeOffset.UtcNow.AddDays(-3);
var marketDataHistory = await api.Public.GetMarketDataHistoryAsync(new OkxPublicMarketDataHistoryQueryRequest
{
    Module = OkxPublicMarketDataHistoryModule.BorrowingRate,
    InstrumentType = OkxInstrumentType.Spot,
    DateAggregationType = OkxPublicDateAggregationType.Daily,
    InstrumentIdList = "ANY",
    Begin = historyEnd.AddDays(-6).ToUnixTimeMilliseconds(),
    End = historyEnd.ToUnixTimeMilliseconds()
});
```

### Historical File Dates vs UTC Instants

The [official historical market-data contract](https://www.okx.com/docs-v5/en/#public-data-rest-api-get-historical-market-data) interprets file and range dates in UTC for order-book modules 4/5/6, and UTC+8 for modules 1/2/3/11. Unix timestamps themselves still represent absolute instants.

`DateRangeStartTime`, `DateRangeEndTime`, and file `Time` remain UTC instants for compatibility. Use the explicit module-aware helpers when selecting files by calendar date:

```csharp
var module = OkxPublicMarketDataHistoryModule.TradeHistory; // Same module as the request.
var rangeStartDate = item.GetDateRangeStartDate(module);
var rangeEndDate = item.GetDateRangeEndDate(module); // Inclusive.
var fileDate = item.groupDetails[0].GetDate(module);
```

These helpers return midnight `DateTime` values with `Kind.Unspecified`: they are dates, not UTC/local instants. Pass the original request module because OKX does not return it in the response; unsupported modules are rejected rather than assigned a guessed timezone. For example, the documented trade file timestamp `1756656000000` is `2025-08-31 16:00:00 UTC`, but its file date is `2025-09-01` in UTC+8. The response-level `Time` is a normal UTC response timestamp, not a file date.

## Tips

- For public trading dashboards, `api.Public` and `api.Rubik` are usually the two most important read-only clients.
- Use typed request overloads when you need many optional filters or when you want future additions to be easier to absorb.
- `GetMarketDataHistoryAsync` requires inclusive `Begin` and `End` timestamps and supports at most 10 inclusive calendar days or months. SPOT queries require `InstrumentIdList`; FUTURES/SWAP/OPTION require `InstrumentFamilyList`. Lists contain at most 10 entries, except module 6 with OPTION, which accepts one family. `ANY` is limited to daily modules 1, 2, 3, and 11.
- Historical market-data timestamps are reduced to their date portion. Order-book modules 4, 5, and 6 use UTC; modules 1, 2, 3, and 11 use UTC+8. Results are newest-first and may be truncated from the beginning when record limits are exceeded. Modules 1, 2, 3, and 11 are normally available on T+2; order books on T+3. The endpoint is limited to 5 requests per 2 seconds per IP.
- Module 5 contains 5000-level order books from November 1, 2025. Module 6 is being deprecated in favor of modules 4 and 5, does not support monthly aggregation, and returns only the day selected by `End` for OPTION.
- Event-contract series, events, and markets REST endpoints are unsigned public requests. Series responses support `five_min`, `fifteen_min`, `hourly`, `daily`, and `monthly` frequencies plus `price_up_down`, `price_above`, `hit`, and `between` settlement methods. Series and markets each use an independent 10-request-per-2-second IP limit.
- Event-contract markets expose `capStrike` for `between` settlement (`INF` means no upper bound) and `hitDir` for `hit` settlement (`up` from below, `dn` from above); the non-applicable field is empty. The `event-contract-markets` WebSocket channel pushes status and floor-strike changes but does not send an initial snapshot, so load the REST markets endpoint before consuming deltas when a complete starting view is required.
- `GetInstrumentsAsync` requires `seriesId` for EVENTS and `instFamily` for OPTION. For OPTION/EVENTS, the returned `tickSz` is only the minimum across the tick bands; use `GetInstrumentTickBandsAsync` for the exact price-range increment.
- Instrument responses preserve the current price-limit percentages, RPI spacing, string-valued fee `groupId`, deprecated auction/category fields, and upcoming-change metadata.
- Query Pre-market X-Perps with `GetInstrumentsAsync(OkxInstrumentType.Futures)` and distinguish the `pre_market` and `xperp` lifecycle phases through `RuleType`; do not infer the product from `InstrumentType` alone.
- OKX renamed `SPACEX-USDT-SWAP` to `SPCX-USDT-SWAP`; the related `uly`, `instFamily`, and `ctValCcy` values changed to `SPCX`, while `instIdCode` remained stable. The wrapper does not silently rewrite instrument IDs. Refresh the instrument catalog and use the current `SPCX-USDT-SWAP`/`SPCX-USDT` values for REST requests and new WebSocket subscriptions.
- During the specific SPACEX-to-SPCX rename, the instruments channel can emit the old ID as `expired`, followed by the new ID as `rebase`, `post_only`, and `live`. Consumers can use that rename's stable `instIdCode` to correlate the symbols; do not generalize code stability to the Crypto-USD-to-Crypto-USDC migration.


