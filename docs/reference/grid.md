# Grid

[Docs Home](../index.md) | [REST Reference](./index.md) | [DCA](./dca.md) | [Signal Bot](./signal-bot.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.Grid` manages OKX grid strategies for both spot and contract grid trading.

All `Grid` methods are private and require credentials.

## Example Calls

```csharp
var place = await api.Grid.PlaceOrderAsync(new OkxGridPlaceOrderRequest
{
    InstrumentId = "BTC-USDT",
    AlgoOrderType = OkxGridAlgoOrderType.SpotGrid,
    MaximumPrice = 5000,
    MinimumPrice = 400,
    GridNumber = 10,
    GridRunType = OkxGridRunType.Arithmetic,
    QuoteSize = 25
});

var history = await api.Grid.GetOrdersHistoryAsync(OkxGridAlgoOrderType.SpotGrid);
var ai = await api.Grid.GetAIParameterAsync(OkxGridAlgoOrderType.SpotGrid, "BTC-USDT");
```

## Method Catalog

### Strategy Lifecycle

- `PlaceOrderAsync`
- `AmendOrderAsync`
- `AmendBasicParametersAsync`
- `StopOrderAsync`
- `ClosePositionAsync`
- `CancelClosePositionAsync`
- `CopyOrderAsync`
- `TriggerOrderAsync`

### Queries

- `GetOpenOrdersAsync`
- `GetOrdersHistoryAsync`
- `GetOrderAsync`
- `GetSubOrdersAsync`
- `GetPositionsAsync`

### Margin and Capital Management

- `WithdrawIncomeAsync`
- `ComputeMarginBalanceAsync`
- `AdjustMarginBalanceAsync`
- `AddInvestmentAsync`

### AI and Planning Helpers

- `GetAIParameterAsync`
- `ComputeMinimumInvestmentAsync`
- `RsiBacktestAsync`
- `GetMaximumGridQuantityAsync`

## Request-Model Overloads

The following `Grid` methods have typed request-model overloads alongside their longer positional signatures:

- `PlaceOrderAsync(OkxGridPlaceOrderRequest)`
- `AmendOrderAsync(OkxGridOrderAmendRequest)`

## Tips

- The grid client has both simple positional overloads and rich request-model flows.
- Contract grid and spot grid parameters differ; prefer the typed request models when building production strategies.


