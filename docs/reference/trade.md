# Trade

[Docs Home](../index.md) | [REST Reference](./index.md) | [Account](./account.md) | [Algo](./algo.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.Trade` is the core order-management client for standard OKX orders.

All `Trade` methods are private and require credentials.

## Example Calls

```csharp
var place = await api.Trade.PlaceOrderAsync(
    "BTC-USDT",
    OkxTradeMode.Cash,
    OkxTradeOrderSide.Buy,
    OkxTradePositionSide.Net,
    OkxTradeOrderType.MarketOrder,
    0.01m);

var order = await api.Trade.GetOrderAsync("BTC-USDT", clientOrderId: "my-order-1");
var openOrders = await api.Trade.GetOpenOrdersAsync();
var tradeHistory = await api.Trade.GetTradesHistoryAsync(OkxInstrumentType.Spot);
```

Example operational calls:

```csharp
await api.Trade.CancelOrderAsync("BTC-USDT", clientOrderId: "my-order-1");
await api.Trade.AmendOrderAsync("BTC-USDT", clientOrderId: "my-order-1");
await api.Trade.ClosePositionAsync("BTC-USDT-SWAP", OkxAccountMarginMode.Cross);
await api.Trade.CancelAllAfterAsync(30);
```

## Method Catalog

### Order Placement and Management

- `PlaceOrderAsync`
- `PlaceOrdersAsync`
- `CancelOrderAsync`
- `CancelOrdersAsync`
- `AmendOrderAsync`
- `AmendOrdersAsync`
- `ClosePositionAsync`

### Order Queries

- `GetOrderAsync`
- `GetOpenOrdersAsync`
- `GetOrderHistoryAsync`
- `GetOrderArchiveAsync`
- `GetTradesAsync`
- `GetTradesHistoryAsync`

### Convert and Repay

- `GetEasyConvertCurrenciesAsync`
- `PlaceEasyConvertOrderAsync`
- `GetEasyConvertHistoryAsync`
- `GetOneClickRepayCurrenciesAsync`
- `PlaceOneClickRepayOrderAsync`
- `GetOneClickRepayHistoryAsync`
- `GetOneClickRepayCurrenciesV2Async`
- `PlaceOneClickRepayOrderV2Async`
- `GetOneClickRepayHistoryV2Async`

### Operational Controls

- `MassCancelAsync`
- `CancelAllAfterAsync`
- `GetAccountRateLimitAsync`
- `OrderPrecheckAsync`

## Request-Model Overloads

The following `Trade` methods have typed request-model overloads in addition to the shorter positional signatures:

- `ClosePositionAsync(OkxTradeClosePositionRequest)`
- `GetOpenOrdersAsync(OkxTradeOpenOrdersRequest)`
- `GetOrderHistoryAsync(OkxTradeOrderQueryRequest)`
- `GetOrderArchiveAsync(OkxTradeOrderQueryRequest)`
- `GetTradesAsync(OkxTradeTransactionQueryRequest)`
- `GetTradesHistoryAsync(OkxTradeTransactionQueryRequest)`
- `OrderPrecheckAsync(OkxTradeOrderPrecheckRequest)`

## Request-Model Examples

```csharp
await api.Trade.ClosePositionAsync(new OkxTradeClosePositionRequest
{
    InstrumentId = "BTC-USDT-SWAP",
    MarginMode = OkxAccountMarginMode.Cross,
    PositionSide = OkxTradePositionSide.Net,
    AutoCancel = true
});

var openOrders = await api.Trade.GetOpenOrdersAsync(new OkxTradeOpenOrdersRequest
{
    InstrumentType = OkxInstrumentType.Swap,
    InstrumentFamily = "BTC-USDT",
    Limit = 50
});

var orderQuery = new OkxTradeOrderQueryRequest
{
    InstrumentType = OkxInstrumentType.Spot,
    InstrumentId = "BTC-USDT",
    Begin = DateTimeOffset.UtcNow.AddDays(-7).ToUnixTimeMilliseconds(),
    Limit = 100
};

var orderHistory = await api.Trade.GetOrderHistoryAsync(orderQuery);
var orderArchive = await api.Trade.GetOrderArchiveAsync(orderQuery);

var tradeQuery = new OkxTradeTransactionQueryRequest
{
    InstrumentType = OkxInstrumentType.Spot,
    InstrumentId = "BTC-USDT",
    Limit = 100
};

var fills = await api.Trade.GetTradesAsync(tradeQuery);
var tradeHistory = await api.Trade.GetTradesHistoryAsync(tradeQuery);

var precheck = await api.Trade.OrderPrecheckAsync(new OkxTradeOrderPrecheckRequest
{
    InstrumentId = "BTC-USDT",
    TradeMode = OkxTradeMode.Cash,
    OrderSide = OkxTradeOrderSide.Buy,
    OrderType = OkxTradeOrderType.LimitOrder,
    Size = 0.01m,
    Price = 50000m
});
```

### RPI Maker Spacing and Amendment

Use the current [REST Place](https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-order), [REST Amend](https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-amend-order), [WS Place](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-order), and [WS Amend](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-order) contracts together with the supplemental [August 11 spacing/visibility notice](https://www.okx.com/docs-v5/log_en/#2026-08-11):

- Cross and organic-price-level checks reference only the **first visible opposite-side RPI**. Hidden RPI is excluded. With no visible opposite-side RPI, the cross check references the opposite organic best bid/offer and the price-level check passes.
- The bps check (`RpiMinimumPriceBand`) always references the opposite organic best price, never an RPI price. `RpiMinimumLevel` exposes the level-spacing threshold; it is not a hidden-order count.
- `RpiPriceRound=true` asks OKX to round outward to a placeable non-crossing level beyond the first visible opposite-side RPI; hidden RPI is excluded from that reference. It defaults to false and is ignored for non-RPI orders and OPTION/EVENTS. The wrapper forwards this choice without altering the requested price locally.
- Amendment is checked against the matching-engine snapshot when the command arrives, with the order being amended still present in the book. Neither public feed is a reliable client-side acceptance oracle.

Amend prices (`newPx`, `newPxUsd`, `newPxVol`) and order IDs are serialized as strings, including REST batch and WS single/batch requests. More than one price representation is rejected before sending. `newSz` is the new **total** size including any fills, not the remaining quantity. `CancelOnFail` defaults server-side to false: a failed amendment preserves the original order unless the caller explicitly requests cancellation.

`OkxTradeOrderAmend.Timestamp`/`Time` represent the original order's creation time (`cTime`), not amendment completion. `sCode=0` acknowledges request acceptance only; use order updates or an order-details query to confirm the final result. RPI taker access is not inherited by an amend; explicitly send it again when needed.

WS single/batch Place and Amend have additive overloads accepting `expiryTimestamp` as an absolute Unix-millisecond deadline. It is sent as a string `expTime` at the command root, not inside `args`, and applies to the entire batch. Existing one-argument overloads omit it; the wrapper does not invent a timeout, reject it based on the local clock, or retry an expired trading request automatically.

Documentation caveats: the WS order-book channel's introductory RPI contract is newer than its generic channel/row/sequence tables; use the explicitly documented `books-rpi` shape rather than the generic third-column `0` description. WS Place/Amend parameter tables do not currently list `attachAlgoOrds`, unlike REST. Existing compatibility serialization is preserved because no explicit removal is documented; this is not a guarantee of WS support.

### Trading Rate-Limit Scope

OKX documents 60 single-order commands per 2 seconds and 300 **orders** per 2 seconds for batches, keyed by User ID + instrument (Options: instrument family). A one-order batch consumes the single-order budget; REST and WS share their corresponding budgets. Lead-trader and sub-account/fill-ratio rules can impose lower limits.

The wrapper's existing REST fallback and per-connection WS throttling do **not** reproduce that full shared/account-aware model. They are not proof that a request is within every server budget. This update preserves those guards rather than raising throughput based on endpoint counts alone. A coordinated limiter requires a separate scoped implementation decision recorded in the [execution contract](../maintenance-plan.md).

## Tips

- Use typed request overloads when you need many filters or optional flags.
- `OrderPrecheckAsync` is useful before placing live orders from automated strategies.
- Current place/amend request models support `ordType=rpi`, `rpiTakerAccess`, and `rpiPxRound`. `rpiTakerAccess` defaults to `false` and is not inherited when amending, so send it again on every amend that should access RPI liquidity. `rpiPxRound=true` lets OKX round a noncompliant RPI maker price outward to the nearest placeable, non-crossing level; it is ignored for non-RPI orders and for OPTION/EVENTS.
- On the private `orders` channel, `AmendSource = RpiPriceRounding` represents wire value `6`: OKX rounded the price to satisfy the RPI maker spacing rule. `OkxTradeOrder` also exposes the channel's current per-update fill fee/P&amp;L, option fill-price metadata, USD notionals, execution role, amend request/result, latest price, and update error fields. Use `ClientRequestId` to deduplicate repeated amendment updates.
- `SlippagePercentage` maps to `slippagePct` for SPOT/SPOT-margin market orders. Pass a decimal fraction from `0` through `0.05` with at most four decimal places (`0.0123` means 1.23%); invalid values are rejected locally by both REST and WebSocket place-order clients.
- `IsElpTakerAccess` and `EnhancedLiquidityProgramOrder` remain only as deprecated OKX transition aliases through October 31, 2026. When both taker-access field names are sent, OKX gives `rpiTakerAccess` precedence; order type values remain mutually exclusive.
- Error `54045` is retired because `rpiTakerAccess` now applies to every standard order type. The wrapper does not retain a synthetic client-side error member for a server code that can no longer be returned.
- `SpeedBump` was removed from the single REST Place order endpoint on July 24, 2026. The wrapper rejects it locally on `PlaceOrderAsync(OkxTradeOrderPlaceRequest)` because OKX would silently ignore it; the shared request property remains available for REST batch and WebSocket single/batch placement, where the current endpoint tables still document it for non-post-only EVENTS orders. Amend requests keep their separate `SpeedBump` property.
- `AttachedAlgoOrders` is documented for REST placement. WS serialization is retained for compatibility, but the current WS parameter tables do not list it; do not treat serialization alone as proof of WS support.
- Contract cool-off is enforced by OKX server-side across REST and WebSocket order placement. While it is active, non-reduce-only orders on affected SWAP/FUTURES instruments are rejected with `54094`; reduce-only orders remain allowed. The wrapper does not cache or predict this account state.
- REST single-order calls expose `54094` as a failed result through `Error.Code`. WebSocket operation acknowledgements and batch responses can carry per-order `sCode`/`sMsg`, so inspect `OkxTradeOrderPlaceResponse.ErrorCode` and `ErrorMessage` for every item even when the top-level operation code is `0`.
- Treat `Easy Convert` and `One Click Repay` methods as account-changing operations.


