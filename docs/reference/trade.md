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

## Crypto-USD to Crypto-USDC Migration

The [September 30 migration notice](https://www.okx.com/docs-v5/log_en/#2026-09-30) delists the affected **SPOT Crypto-USD** instruments in favor of their corresponding Crypto-USDC instruments. This is not a blanket rewrite of every USD futures/swap/options symbol. Old `instId`/`instIdCode` values are **not mapped server-side** and can fail or return no data; requests, subscriptions, response parsing, instrument metadata, configured limiter registrations, and all keyed caches must use the actual new IDs/codes. The wrapper does not rewrite them or infer a stable code/alias.

Select trading currency from the current private [Account instruments](https://www.okx.com/docs-v5/en/#trading-account-rest-api-get-instruments) `TradeQuoteCurrencyList`. The existing REST positional/model/batch and WS single/batch placement paths preserve the caller's optional `tradeQuoteCcy` independently for every order. REST uses the actual `InstrumentId`; WS uses its actual integer `InstrumentIdCode`, with no legacy-ID translation.

| Quote choice on the new Crypto-USDC SPOT instrument | Wire value | Server default/intent |
| --- | --- | --- |
| Continue USD trading, if supported by the account | `TradeQuoteCurrency = "USD"` | Explicit USD; omission does not preserve the old USD default |
| Explicit USDC trading | `TradeQuoteCurrency = "USDC"` | Explicit USDC |
| Leave quote unspecified | null, field omitted | New instrument's USDC quote currency, subject to regional/account rules |

Some regions require an explicit quote currency and can return `51000` when it is omitted; the REST Place contract requires provided values to belong to the account's `tradeQuoteCcyList`. The current placement tables still illustrate the default with `BTC-USD`; this is an example, not a guarantee that an affected delisted pair or its old default remains available. Sources: current [REST single](https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-order), [REST batch](https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-multiple-orders), [WS single](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-order), and [WS batch](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-multiple-orders) contracts, supplemented by the migration notice.

When placement returns `54109`, consider the explicit [activation method](./account.md#explicit-usdc-feature-activation) under the account owner's control. Trade methods preserve actual per-item rejection and mixed-batch accepted outcomes; they do not activate features or resend orders. Always inspect every item's `ErrorCode` (`sCode`), including when WS code-0 `Success` is true. Do not replay an accepted batch item while handling another item's activation requirement, or blindly retry an uncertain outcome. No funds are converted/transferred and no account or existing order is changed by migration guidance.

The 49 new September 30 cases use local signed REST requests and captured WS commands/pushes. They verify forwarding, omission, actual codes, incremental catalog handling, activation errors/limits, and no automatic replay—not live eligibility, server migration timing, or global rate-limit enforcement. Existing trading limiter and documentation-conflict boundaries below remain unchanged.

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

### RPI Maker Minimum Notional

The [August 18 notice](https://www.okx.com/docs-v5/log_en/#2026-08-18) introduces the server-side RPI minimum. The current [September 15 thresholds](https://www.okx.com/docs-v5/log_en/#2026-09-15), checked on October 1, 2026, supersede the original values:

| Product | Minimum RPI maker notional |
| --- | --- |
| SPOT | 500 USD |
| FUTURES | 2,000 USD |
| SWAP | 5,000 USD |

- The minimum applies to `ordType=rpi` and the still-accepted legacy `elp` alias. Non-RPI orders, including takers with `RpiTakerAccess=true`, are not subject to this maker minimum. The August notice lists EVENTS as not applicable; it does not define an OPTION threshold, so the wrapper invents none.
- Instrument `minSz` and the USD-notional minimum are independent requirements. Meeting either one does not imply meeting the other.
- Placement below the threshold is rejected with `54051`. An RPI/ELP amendment containing `newSz`, with or without a new price, revalidates the amended quantity. A price-only amendment omitting `newSz` does not trigger this minimum-notional recheck; it must still satisfy other price/spacing rules.
- Existing resting orders are not retroactively rejected by this change. A failed amendment preserves the original order with the default `CancelOnFail=false`; the current REST Amend tables explicitly say `true` auto-cancels on **any** amendment failure. Do not turn the announcement's unqualified preservation statement into a guarantee when opting into cancellation.
- Threshold checks remain on OKX. Derivative size is a number of contracts, not a coin amount; the documented notional calculation depends on contract metadata and prices, and USD conversion must not be guessed. The wrapper adds no local `price * size` oracle, invented instrument field, automatic resizing, price adjustment, cancellation, or retry.

The complete current single-order contracts linked above and [REST batch Place](https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-place-multiple-orders), [REST batch Amend](https://www.okx.com/docs-v5/en/#order-book-trading-trade-post-amend-multiple-orders), [WS batch Place](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-place-multiple-orders), and [WS batch Amend](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-amend-multiple-orders) were compared again. Their current parameter tables add no minimum-notional request/response field and omit the detailed minimum behavior; use the notices as supplemental behavior evidence. The current [trade error table](https://www.okx.com/docs-v5/en/#error-code-rest-api-public-trade-class) independently confirms `54051` and its server-supplied USD threshold message. There is no static error catalog in this wrapper; existing numeric errors and per-item codes expose the rejection directly.

The dedicated [September 15 verification step](../maintenance-plan.md#september-15--2026-10-01) re-fetched and compared these eight complete contracts, not only the thresholds. No additional production-code change was identified: the current thresholds and outcome handling were already covered by the August 18 work. All 170 focused existing cases and all 715 offline tests passed; package metadata remains 5.6.826. This verifies client parsing/forwarding, not live notional enforcement, default shared-budget parity, or resolution of the documentation conflicts below.

### Place/Amend Acknowledgements and Partial Failure

Inspect every returned item's `ErrorCode` (`sCode`), `ErrorMessage` (`sMsg`), and `SubCode`, not just the command's `Success`. `54051` is an individual rejection, not permission to resend every item in a batch.

- REST single Place/Amend exposes an individual rejection through numeric `Error.Code`, including `54051`, with the server message and sub-code preserved. The acknowledgement item is also retained in `Data` when available, including failure acknowledgements.
- REST batch `code=0` retains every acknowledgement and reports command-level `Success=true`, even if one or all items have nonzero `sCode`. The first item no longer changes the handling of the whole batch. Inspect all items, including in a one-order batch.
- The current WS batch examples explicitly define aggregate `code=2` for partial success and `code=1` for all failed. Both report `Success=false` and numeric aggregate `Error.Code`, but **retain all per-order `Data`**. The same preservation applies if REST returns these aggregate codes with valid acknowledgement items; the REST endpoint tables themselves only describe `0` as success.
- WS single failures with aggregate `code=1` retain the acknowledgement and expose the item's numeric rejection, message, and sub-code. For compatibility, a WS `code=0` acknowledgement still reports command-level success even if its item contains a rejection; inspect the item's `ErrorCode` in all cases.
- Gateway/protocol errors such as `60013` are not mistaken for aggregate order outcomes and do not manufacture order `Data`. Empty aggregate-error arrays likewise remain dataless. WS replies with `id` and `op` must match both request ID and operation, including single versus batch operations. Unidentified subscription `event:error` messages do not complete trading queries; missing order outcomes remain uncertain rather than being replaced by an unrelated error.
- `inTime` and `outTime` are gateway **microsecond** timestamps, distinct from the item's millisecond creation `ts`. REST callers can retain the full JSON envelope using `OkxRestApiOptions.RawResponse=true`; correlated WS query results retain the decoded JSON envelope in `CallResult.Raw`, including aggregate errors, request ID, operation, and gateway times. These envelope fields are not copied onto each order item as if they were per-order fields.

Always check `Data` for available outcomes even when `Success=false`. `GetResultOrError` and implicit success checks do not expose failure-associated data. An accepted placement or amendment acknowledgement is not proof of final execution; reconcile with the private order channel or an order-details query. Never blindly retry a mixed or uncertain batch: accepted orders may already be live.

Documentation conflict: the August 18 notice describes independent placement-suborder rejection, while current REST/WS batch Place tables retain an all-accepted-or-all-rejected statement for Portfolio Margin. The wrapper preserves the actual acknowledgements and does not promise atomicity or independently simulate either server behavior. Synthetic tests verify parsing/forwarding, not matching-engine enforcement or Portfolio Margin acceptance.

The current REST Amend and WS batch Amend response examples contain missing JSON commas. Treat their field tables as schema evidence and use valid synthetic JSON in tests; malformed documentation examples are not evidence that OKX sends malformed live payloads.

### WebSocket Response Correlation and Confirmation

The October 1 [review remediation](../maintenance-plan.md#review-remediation--2026-10-01) fixes the three shared-handler defects found at the 5.6.820 checkpoint. The follow-up is included in the 5.6.826 source/package build; previously built/installed 5.6.820 packages are not evidence that these fixes are present. No package publication or application deployment is implied.

- Subscription errors cannot complete an unrelated Place/Amend query, nor can login replies substitute for trading acknowledgements. Trading responses still require their documented ID/operation. Reconcile uncertain outcomes by `ordId`/`clOrdId`; never retry blindly.
- Every subscribe attempt supplies an ID at the request root and requires its echo. A fresh 32-character alphanumeric ID is generated when absent; explicit IDs remain unchanged and must be unique. Stored requests are not mutated, so SDK reconnect attempts receive fresh generated IDs and confirmation state. Missing/foreign IDs are not guessed from error-message text.
- Multi-argument subscriptions confirm only after all distinct requested arguments have exact ACK matches. Duplicate or reordered ACKs are supported without counting duplicates toward another filter; pushes remain governed by the separate ANY/type/family/instrument routing rules. Correlated failures retain numeric errors/raw JSON even after partial ACKs. Missing ACKs report an uncertain failure, not success. Completed/timed-out subscription and unsubscription wait handlers ignore late responses; this protection does not yet cover authentication callbacks.
- The protected orders-only unsubscribe helper uses a fresh ID and waits for every argument's confirmation. The [orders table](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-order-channel) documents ID for subscribe/unsubscribe, but the [general unsubscribe table](https://www.okx.com/docs-v5/en/#overview-websocket-unsubscribe) does not. Other channels retain ID-less requests and exact argument matching; simultaneous identical ID-less unsubscribe filters cannot be distinguished by ID. Unidentified errors do not manufacture a successful helper result. This result is not preserved by the SDK's public unsubscribe/local-close path.

Intermediate valid ACKs are debug-only protocol notifications, not synthetic order callbacks or whole-subscription success. The existing SDK connection/reconnect/failed-local-subscription lifecycle is retained. A partial failure or timeout does **not** prove no remote subscriptions are active; no compensating cleanup or new automatic retry/reconnection was added. Deliberately reconcile/restabilize state, and remember the orders channel has no initial snapshot. The 55 new local regressions verify client behavior, not live server enforcement.

The [final cross-surface review](../maintenance-plan.md#final-cross-surface-review--2026-10-01) reproduced two still-open P2 integration gaps. An expired login callback can consume a late successful reply and set the wrapper's public `IsAuthendicated` flag even though the completed attempt returned failure; this does not establish that the SDK's separate connection authentication state changed. Do not treat that flag after an uncertain/expired login as proof of current connection readiness.

Public `UnsubscribeAsync(subscriptionId)` returns true after finding and locally closing the subscription even when the protected helper received incomplete/rejected confirmation. The subscription-object overload returns Task without exposing a confirmation result. SDK cleanup marks/removes the local subscription regardless of the helper's bool; on a shared open connection, remote removal remains uncertain. Awaiting these public calls is therefore a local-close operation, not proof that OKX acknowledged every requested filter. Existing protected-helper tests do not exercise that public result-loss path. Bounded fixes and actual public-path regressions are proposed, not implemented by this review; no automatic cleanup/retry/reconciliation or order replay is added.

### Orders Channel: Placement, State, and Reconciliation

The complete current [private orders channel](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-order-channel) was compared on October 1, 2026. Its generic field tables do not fully describe the placement-notification change, so also use the [August 20 notice](https://www.okx.com/docs-v5/log_en/#2026-08-20). An accepted Place/Amend acknowledgement is not a final order state.

For `post_only`, `mmp_and_post_only`, and RPI placement, OKX sends `live` after book entry, not immediately on receipt. The approximate 1 ms delay is not a timeout or SLA. Failure may produce **only `canceled`**, without preceding `live`. A later cancellation remains possible after a genuinely live order; book entry is not a promise of execution.

| Placement scenario | Server update sequence |
| --- | --- |
| Post-only crosses the BBO | `canceled` only |
| Maker order rests successfully | `live` |
| Resting post-only then fully filled | `live` → `filled` (possibly through `partially_filled`) |
| Reduce-only post-only quantity overridden | `live` with `amendSource=4`, `amendResult=0` → `live` |
| RPI spacing failure with rounding disabled | `canceled` only |
| RPI price rounded by OKX | `live` with `amendSource=6`, `amendResult=0` → `live` |

The RPI behavior also covers legacy `elp` during its transition. Ordinary `limit`, `market`, `ioc`, and `fok` behavior is unchanged. `SystemReduceOnly` documents both new-order size overrides and reduction of existing pending orders. `CancelSource` remains a raw string: current reasons include `31` (post-only would take liquidity), `45` (RPI price verification failed), and `39` (MMP-triggered cancellation). Channel `code=0` does not turn `canceled` into a successful live order; `MmpCanceled` remains distinct.

`ws.Trade.SubscribeToOrderUpdatesAsync` uses the authenticated private endpoint and has **no initial snapshot**. Subscription types are SPOT, MARGIN, SWAP, FUTURES, OPTION, EVENTS, and ANY; optional family filters apply to FUTURES/SWAP/OPTION. Orders push routing accepts concrete instrument metadata within an ANY/type/family subscription while enforcing explicit type/family/instrument filters. Subscribe/unsubscribe acknowledgements still match exact arguments; other channels' matching is unchanged.

Callbacks forward rows without synthetic states, delays, reordering, caching, or deduplication. Preserve the first amendment-bearing `live` update and following `live`, even for the same order/state/timestamp. Application-side reconciliation should follow the channel guidance:

- Deduplicate fills by instrument ID plus nonempty `TradeId`, not timestamp or order ID alone.
- For a SPOT/MARGIN market terminal `filled` update without a trade ID, process the first terminal fill per order ID. `FillQuantity=0` in a market `filled` update does not mean it never filled.
- Process only the first `canceled`/`mmp_canceled` terminal cancellation per order ID.
- Deduplicate user amendment notifications by nonempty `ClientRequestId`; do not collapse system adjustments solely by order ID/state.

`OkxTradeOrder.RiskBypassResult` preserves the string without interpretation; account-specific meanings require OKX's relationship manager. Missing/not-applicable values remain empty. `auto_conversion` is mapped without changing existing enum numbers. Shared envelope models retain optional `id`/`connId` and `arg.instType`/`arg.instFamily`; the per-order callback signature is unchanged and does not expose the entire envelope. Fee/rebate comments reflect SPOT/MARGIN maker-sell quote/base currencies and signed accumulated amounts; values are not recalculated. The orders channel reports `AveragePrice=0` before fills.

The linked [connection-count contract](https://www.okx.com/docs-v5/en/#overview-websocket-connection-count-limit) allows 30 connections per affected channel per sub-account, not 30 subscription arguments. Multiple orders filters on one connection count once. Register `ws.ChannelConnectionCount` before subscribing: `channel-conn-count-error` can follow a successful acknowledgement and means that connection's channel subscription was terminated. `IsLimitError`, `Channel`, `ConnectionId`, and the server count expose this notice; a count update alone is not a termination. The wrapper does not infer sub-account identity, enforce a process-global quota, automatically resubscribe, or resend commands. Restore subscriptions and reconcile state deliberately. Trading commands are not themselves restricted by this channel connection-count rule.

Expired OPTION closing orders do not produce orders-channel closing updates according to the current contract; reconcile expiry state separately. Documentation caveat: `linkedAlgoOrd.algoId` is labeled Object in the table but is a string in payload examples; the string representation is preserved. Prior WS attached-order and acknowledgement caveats remain applicable. Local tests do not prove matching-engine timing or private connection-limit enforcement.

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


