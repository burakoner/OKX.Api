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
var history = await api.Algo.GetOrderHistoryAsync(
    OkxAlgoOrderType.Conditional,
    algoOrderState: OkxAlgoOrderState.Canceled);
```

Trigger-to-chase orders are not the same contract as standalone chase orders. A trigger-to-chase request sets `ordType=trigger`, `advanceOrdType=chase`, and sends chase values inside `advChaseParams`:

```csharp
var triggerChase = await api.Algo.PlaceOrderAsync(
    "BTC-USDT-SWAP",
    OkxTradeMode.Cross,
    OkxTradeOrderSide.Buy,
    OkxAlgoOrderType.Trigger,
    size: 2m,
    triggerPrice: 90000m,
    triggerPriceType: OkxAlgoPriceType.Mark,
    triggerOrderType: OkxAlgoTriggerOrderType.Chase,
    advancedChaseParameters:
    [
        new()
        {
            ChaseType = OkxAlgoChaseType.Distance,
            ChaseValue = 25m,
            MaximumChaseType = OkxAlgoChaseType.Ratio,
            MaximumChaseValue = 0.02m
        }
    ]);
```

For this form, `orderPrice` and `attachedAlgoOrders` are invalid. Before the trigger fires, `OkxAlgoOrder.SubAlgoIdList` is empty. After it fires, that list contains the spawned chase algo ID and `OrderIdList` remains empty. Standalone `ordType=chase` continues to use the root `chaseType`, `chaseVal`, `maxChaseType`, and `maxChaseVal` arguments.

Only `NewChaseValue` and `NewMaximumChaseValue` can be amended before the trigger fires. Chase types cannot be changed. The server also prevents switching the original chase value between direct best-price tracking (`0`) and distance mode (`>0`), and allows a maximum amendment only when the original request enabled one.

The same current Place/List/History contracts now expose smart iceberg orders:

```csharp
var smartIceberg = await api.Algo.PlaceOrderAsync(
    "BTC-USDT-SWAP",
    OkxTradeMode.Cross,
    OkxTradeOrderSide.Buy,
    OkxAlgoOrderType.SmartIceberg,
    size: 100m,
    sizeLimit: 5m,
    priceLimit: 85000m,
    limitOrderNumber: 20,
    aggressiveness: OkxAlgoSmartIcebergAggressiveness.Mid,
    smartIcebergTriggerParameters:
    [
        new()
        {
            TriggerAction = OkxAlgoSmartIcebergTriggerAction.Start,
            TriggerStrategy = OkxAlgoSmartIcebergTriggerStrategy.RSI,
            TriggerCondition = OkxAlgoSmartIcebergTriggerCondition.CrossDown,
            TimeFrame = OkxAlgoSmartIcebergTimeFrame.ThirtyMinutes,
            Threshold = 30,
            TimePeriod = 14
        }
    ]);
```

An omitted or empty `smartIcebergTriggerParameters` list starts immediately. For RSI triggers, the threshold range is 1–100 and the current fixed calculation period is 14.

Pending and historical queries also accept the one documented multi-type filter:

```csharp
var stops = await api.Algo.GetOpenOrdersAsync(
    [OkxAlgoOrderType.Conditional, OkxAlgoOrderType.OCO]);
```

No other order-type combination is valid. History requires either `algoOrderState` (`Effective`, `Canceled`, or `Failed`) or `algoId`. Both query endpoints accept only SPOT, MARGIN, SWAP, and FUTURES instrument filters.

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
- `advanceOrdType=chase` is currently limited by OKX to FUTURES and SWAP. The Place endpoint accepts only `instId`, not `instType`, so the client cannot prove the product type locally; OKX performs that check.
- The REST details/list/history endpoints and `orders-algo` WebSocket channel share `OkxAlgoOrder`, including `AdvancedChaseParameters`, `SubAlgoIdList`, smart iceberg fields, WebSocket amendment metadata, and the current order tag.

Official contracts: [Place algo order](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-place-algo-order), [Amend algo order](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-post-amend-algo-order), [Algo order details](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-details), [Algo order list](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-list), [Algo order history](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-get-algo-order-history), and [Algo orders channel](https://www.okx.com/docs-v5/en/#order-book-trading-algo-trading-ws-algo-orders-channel).


