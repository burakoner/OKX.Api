# Recurring Buy

[Docs Home](../index.md) | [REST Reference](./index.md) | [DCA](./dca.md) | [Signal Bot](./signal-bot.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.RecurringBuy` manages recurring buy strategies.

All `RecurringBuy` methods are private and require credentials.

## Example Calls

```csharp
var place = await api.RecurringBuy.PlaceOrderAsync(
    "Strategy Name",
    new List<OkxRecurringBuyItem>(),
    OkxRecurringBuyPeriod.Monthly,
    1,
    null,
    1,
    "UTC",
    1000m,
    "USDT",
    OkxTradeMode.Cross);

var open = await api.RecurringBuy.GetOpenOrdersAsync();
var history = await api.RecurringBuy.GetOrderHistoryAsync();
var order = await api.RecurringBuy.GetOrderAsync(123456789);
```

## Method Catalog

- `PlaceOrderAsync`
- `AmendOrderAsync`
- `StopOrderAsync`
- `AmendRecurringTimeAsync`
- `AmendRecurringAmountAsync`
- `AddInvestmentAsync`
- `PauseAsync`
- `RestartAsync`
- `AmendPriceRangeAsync`
- `GetOpenOrdersAsync`
- `GetOrderHistoryAsync`
- `GetOrderAsync`
- `GetSubOrdersAsync`

## Tips

- Recurring buy methods operate on strategy IDs (`algoOrderId`) rather than ordinary order IDs.
- The matching WebSocket private order-updates flow is documented in the examples project and uses authenticated business sockets.


