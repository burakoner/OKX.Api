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

## Tips

- DCA strategies are account-changing operations; do not treat examples as safe read-only calls.
- Use the typed request models for production code because DCA payloads evolve frequently.


