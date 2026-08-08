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

## Tips

- Use typed request overloads when you need many filters or optional flags.
- `OrderPrecheckAsync` is useful before placing live orders from automated strategies.
- Current place/amend request models support `ordType=rpi`, `rpiTakerAccess`, and `rpiPxRound`. `rpiTakerAccess` defaults to `false` and is not inherited when amending, so send it again on every amend that should access RPI liquidity. `rpiPxRound=true` lets OKX round a noncompliant RPI maker price outward to the nearest placeable, non-crossing level; it is ignored for non-RPI orders and for OPTION/EVENTS.
- `SlippagePercentage` maps to `slippagePct` for SPOT/SPOT-margin market orders. Pass a decimal fraction from `0` through `0.05` with at most four decimal places (`0.0123` means 1.23%); invalid values are rejected locally by both REST and WebSocket place-order clients.
- `IsElpTakerAccess` and `EnhancedLiquidityProgramOrder` remain only as deprecated OKX transition aliases through October 31, 2026. When both taker-access field names are sent, OKX gives `rpiTakerAccess` precedence; order type values remain mutually exclusive.
- `SpeedBump` was removed from the single REST Place order endpoint on July 24, 2026. The wrapper rejects it locally on `PlaceOrderAsync(OkxTradeOrderPlaceRequest)` because OKX would silently ignore it; the shared request property remains available for REST `PlaceOrdersAsync`, whose current endpoint table still documents it. Amend requests keep their separate `SpeedBump` property.
- Contract cool-off is enforced by OKX server-side across REST and WebSocket order placement. While it is active, non-reduce-only orders on affected SWAP/FUTURES instruments are rejected with `54094`; reduce-only orders remain allowed. The wrapper does not cache or predict this account state.
- REST single-order calls expose `54094` as a failed result through `Error.Code`. WebSocket operation acknowledgements and batch responses can carry per-order `sCode`/`sMsg`, so inspect `OkxTradeOrderPlaceResponse.ErrorCode` and `ErrorMessage` for every item even when the top-level operation code is `0`.
- Treat `Easy Convert` and `One Click Repay` methods as account-changing operations.


