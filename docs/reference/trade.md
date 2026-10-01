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

The [trading rate-limit rules](https://www.okx.com/docs-v5/en/#overview-rate-limits-trading-related-apis) and current single/batch endpoint tables document 60 single-order commands per 2 seconds and 300 **orders** per 2 seconds for batches, keyed by User ID + instrument (Options: the official `instFamily`, not a family guessed from the symbol). A one-order batch consumes the single-order budget. Place and Amend are independent; REST and WS share their corresponding budgets. Lead instruments for a Copy Trading lead account use 4 commands/orders per 2 seconds on their respective single/batch endpoints.

`OkxTradeRateLimiter` is an **opt-in, fail-fast** Place/Amend guard. Assign the **same instance** to `OkxRestApiOptions.TradeRateLimiter` and `OkxWebSocketApiOptions.TradeRateLimiter` for all clients/API keys of one User ID in one environment. It does not infer User ID, lead status, or production/demo equivalence from credentials. Register every current instrument with `RegisterInstrument(instrument, isLeadInstrument)`; registration snapshots metadata so later caller mutations cannot change the key. Unknown IDs/codes and Options without `instFamily` fail before sending; WS codes never silently fall back to an ID.

```csharp
// currentInstrument comes from current official instrument metadata in this environment.
// isLeadForThisAccount must be explicitly known from this account's Copy Trading configuration.
var guard = new OkxTradeRateLimiter(); // Conservative documented base: 1000 account orders / 2 seconds.
guard.RegisterInstrument(currentInstrument, isLeadForThisAccount);

var rest = new OkxRestApiClient(new OkxRestApiOptions(credentials) { TradeRateLimiter = guard });
var socket = new OkxWebSocketApiClient(new OkxWebSocketApiOptions
{
    ApiCredentials = credentials,
    TradeRateLimiter = guard
});
```

The guard atomically reserves every instrument/family and account weight for a command; a rejected mixed batch consumes no partial budget and is not split or resent. A command is counted throughout existing transport queues and its response wait, then for two seconds **after completion**, using a monotonic clock. This conservative window prevents premature release before a delayed send. Exceptions, cancellation after reservation, and rejected/uncertain submissions do not immediately refund usage. Exhaustion returns `ClientRateLimitError` without sending; it is a local guard error, not a fabricated server `50011`. There is no new wait queue, automatic retry, price adjustment, or deadline adjustment.

The [account-limit rules](https://www.okx.com/docs-v5/en/#overview-rate-limits-fill-ratio-based-sub-account-rate-limit) provide a base aggregate budget of 1000 orders per two seconds, with current VIP/fill-ratio tiers potentially raising it. Each batch order counts individually across Place and Amend. SPOT/MARGIN and known MMP placements are exempt from this account budget but still consume their instrument/family budget. Amend inputs do not reveal the original order type, so otherwise non-exempt MMP amendments are **conservatively counted**; no original type is inferred. Block/spread operations and cancellation are not routed through this Place/Amend guard.

`GetAccountRateLimitAsync` now has the documented [1 request/second guard](https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-account-rate-limit) on the existing REST limiter instance; coordinate these queries across separately configured REST clients too. Reading it does not silently reconfigure the shared Place/Amend guard. Explicitly apply a fresh current `AccountRateLimit` via `SetAccountRateLimit`, keeping any lower application cap; do not apply `NextAccountRateLimit` early or predict VIP transitions. Updating the account cap or registered code/lead classification preserves usage. Register updated instrument metadata/lead state before subsequent commands; rate-key changes to an existing instrument are rejected rather than resetting its quota.

With `TradeRateLimiter=null` (the default), legacy behavior remains; the REST fallback and per-connection WS throttles alone do **not** reproduce the shared/account-aware contract. Those existing guards are retained even when the new guard is enabled, so this is not a throughput increase. It cannot observe other processes, other separately configured instances, or server state such as the maximum three amendments in progress per order (`51513`). It is not proof that OKX will accept an order or that no server rate error can occur. Keep one guard for the same account/environment, including while commands are in flight; replacing it resets locally observed usage. Keep that binding consistent when credentials change, and use a separate guard before using a different account/environment. The [execution contract](../maintenance-plan.md) records these intentional boundaries.

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


