# DCA

[Docs Home](../index.md) | [REST Reference](./index.md) | [Grid](./grid.md) | [Recurring Buy](./recurring-buy.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.DCA` manages dollar-cost averaging strategies exposed by OKX.

All `DCA` methods are private and require credentials.

## Example Calls

```csharp
var place = await api.DCA.PlaceOrderAsync(new OkxDcaCreateOrderRequest());
var open = await api.DCA.GetOpenOrdersAsync();
var details = await api.DCA.GetPositionDetailsAsync(123456789);
var cycles = await api.DCA.GetCyclesAsync(123456789);
```

## Method Catalog

### Strategy Lifecycle

- `PlaceOrderAsync`
- `AmendOrderAsync`
- `StopOrderAsync`
- `GetOrderAsync`
- `GetOpenOrdersAsync`
- `GetOrderHistoryAsync`
- `GetSubOrdersAsync`

### Strategy Operations

- `ManualBuyAsync`
- `AmendReinvestmentAsync`
- `AmendTakeProfitAsync`
- `GetPositionDetailsAsync`
- `GetCyclesAsync`
- `AddMarginAsync`
- `ReduceMarginAsync`

## Request-Model Overloads

The following `DCA` methods have typed request-model overloads alongside their longer positional signatures:

- `PlaceOrderAsync(OkxDcaCreateOrderRequest)`
- `AmendOrderAsync(OkxDcaAmendOrderRequest)`

## Request-Model Examples

```csharp
var place = await api.DCA.PlaceOrderAsync(new OkxDcaCreateOrderRequest
{
    InstrumentId = "BTC-USDT",
    AlgoOrderType = OkxDcaAlgoOrderType.SpotDca,
    InitialOrderAmount = 25m,
    MaximumSafetyOrders = 3,
    TakeProfitTarget = 0.05m,
    TriggerParameters =
    [
        new OkxDcaTriggerParametersRequest
        {
            TriggerAction = OkxDcaTriggerAction.Start,
            TriggerStrategy = OkxDcaTriggerStrategy.Instant
        }
    ]
});

var amend = await api.DCA.AmendOrderAsync(new OkxDcaAmendOrderRequest
{
    AlgoOrderId = 123456789,
    PriceStepRatio = 0.01m,
    PriceStepMultiplier = 1.2m,
    VolumeMultiplier = 1.1m,
    TakeProfitTarget = 0.05m,
    StopLossTarget = 0.03m,
    InitialOrderAmount = 25m,
    SafetyOrderAmount = 15m,
    MaximumSafetyOrders = 3,
    ReserveFunds = true
});
```

## Tips

- DCA strategies are account-changing operations; do not treat examples as safe read-only calls.
- Use the typed request models for production code because DCA payloads evolve frequently.


