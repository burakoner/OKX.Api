# Spread Trading

[Docs Home](../index.md) | [REST Reference](./index.md) | [Block](./block.md) | [Trade](./trade.md)

Official OKX docs: [Spread Trading](https://www.okx.com/docs-v5/en/#spread-trading)

## Overview

`api.Spread` combines private spread order management with public spread market data.

It is a mixed section:

- order management requires credentials
- order book, ticker, trade, and candle reads are public

## Example Calls

```csharp
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var place = await api.Spread.PlaceOrderAsync(
    spreadId: "BTC-USDT_BTC-USD-SWAP",
    side: OkxTradeOrderSide.Buy,
    type: OkxSpreadOrderType.Limit,
    size: 1m,
    price: 10m);

var open = await api.Spread.GetOpenOrdersAsync();
var history = await api.Spread.GetOrderHistoryAsync();
```

Public examples:

```csharp
var spreads = await api.Spread.GetSpreadsAsync();
var orderBook = await api.Spread.GetOrderBookAsync("BTC-USDT_BTC-USD-SWAP");
var ticker = await api.Spread.GetTickerAsync("BTC-USDT_BTC-USD-SWAP");
var trades = await api.Spread.GetTradesAsync("BTC-USDT_BTC-USD-SWAP");
```

## Method Catalog

### Private Trading

- `PlaceOrderAsync`
- `CancelOrderAsync`
- `CancelOrdersAsync`
- `AmendOrderAsync`
- `GetOrderAsync`
- `GetOpenOrdersAsync`
- `GetOrderHistoryAsync`
- `GetOrderArchiveAsync`
- `GetTradesAsync`
- `CancelAllAfterAsync`

### Public Spread Market Data

- `GetSpreadsAsync`
- `GetOrderBookAsync`
- `GetTickerAsync`
- `GetTradesAsync`
- `GetCandlesticksAsync`
- `GetCandlesticksHistoryAsync`

## Request-Model Overloads

The following `Spread` methods have typed request-model overloads in addition to the shorter positional signatures:

- `PlaceOrderAsync(OkxSpreadRestOrderPlaceRequest)`
- `AmendOrderAsync(OkxSpreadRestOrderAmendRequest)`
- `GetOpenOrdersAsync(OkxSpreadOrderQueryRequest)`
- `GetOrderHistoryAsync(OkxSpreadOrderQueryRequest)`
- `GetOrderArchiveAsync(OkxSpreadOrderQueryRequest)`
- `GetTradesAsync(OkxSpreadTradeQueryRequest)` for private spread trade history
- `GetSpreadsAsync(OkxSpreadInstrumentQueryRequest)`
- `GetCandlesticksAsync(OkxSpreadCandlestickRequest)`
- `GetCandlesticksHistoryAsync(OkxSpreadCandlestickRequest)`

## Tips

- The private and public `GetTradesAsync` methods are different overload families; use the one that matches your input arguments.
- `CancelOrdersAsync` performs mass cancel behavior for the spread section.


