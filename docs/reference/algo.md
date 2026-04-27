# Algo

[Docs Home](../index.md) | [REST Reference](./index.md) | [Trade](./trade.md) | [Grid](./grid.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.Algo` covers OKX algo-order workflows such as conditional and advanced order types.

All `Algo` methods are private and require credentials.

## Example Calls

```csharp
var place = await api.Algo.PlaceOrderAsync(
    "BTC-USDT",
    OkxTradeMode.Isolated,
    OkxTradeOrderSide.Sell,
    OkxAlgoOrderType.Conditional);

var order = await api.Algo.GetOrderAsync(algoOrderId: 1000001);
var open = await api.Algo.GetOpenOrdersAsync(OkxAlgoOrderType.OCO);
var history = await api.Algo.GetOrderHistoryAsync(OkxAlgoOrderType.Conditional);
```

## Method Catalog

- `PlaceOrderAsync`
- `CancelOrderAsync`
- `CancelOrdersAsync`
- `AmendOrderAsync`
- `GetOrderAsync`
- `GetOpenOrdersAsync`
- `GetOrderHistoryAsync`

## Tips

- Keep algo order identifiers and client identifiers in your own storage; they matter for cancel and amend flows.
- If you need regular limit or market orders, use `api.Trade` instead of `api.Algo`.


