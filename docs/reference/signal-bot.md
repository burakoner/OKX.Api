# Signal Bot

[Docs Home](../index.md) | [REST Reference](./index.md) | [Recurring Buy](./recurring-buy.md) | [Copy Trading](./copy-trading.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.SignalBot` covers signal channels, signal bot orders, positions, and sub-orders.

All `SignalBot` methods are private and require credentials.

## Example Calls

```csharp
var channel = await api.SignalBot.CreateSignalAsync("trend-following", "channel for automated signals");
var channels = await api.SignalBot.GetSignalsAsync(OkxSignalBotSourceType.FreeSignal);
var order = await api.SignalBot.GetSignalBotOrderAsync(123456789);
var active = await api.SignalBot.GetActiveSignalBotOrdersAsync(123456789);
var positions = await api.SignalBot.GetPositionsAsync(123456789);
```

## Method Catalog

### Channels and Signal Definitions

- `CreateSignalAsync`
- `CreateChannelAsync`
- `GetSignalsAsync`
- `GetChannelsAsync`

### Signal Bot Orders

- `CreateSignalBotAsync`
- `CancelSignalBotsAsync`
- `AdjustMarginBalanceAsync`
- `AmendTPSLAsync`
- `SetInstrumentsAsync`
- `GetSignalBotOrderAsync`
- `GetActiveSignalBotOrdersAsync`
- `GetSignalBotHistoryAsync`

### Positions and Sub Orders

- `GetPositionsAsync`
- `GetPositionHistoryAsync`
- `ClosePositionAsync`
- `PlaceSubOrderAsync`
- `CancelSubOrderAsync`
- `GetSubOrdersAsync`
- `GetEventHistoryAsync`

## Tips

- `CreateSignalAsync` and `CreateChannelAsync` are aliases around the same concept; use the name that best matches your codebase.
- Signal bot endpoints are strategy endpoints, not plain spot or derivatives order endpoints.


