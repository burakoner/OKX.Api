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
- Treat `Easy Convert` and `One Click Repay` methods as account-changing operations.


