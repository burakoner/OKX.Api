# Copy Trading

[Docs Home](../index.md) | [REST Reference](./index.md) | [Signal Bot](./signal-bot.md) | [Affiliate](./affiliate.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.CopyTrading` contains both lead-trader and copy-trader workflows.

All `CopyTrading` methods are private and require credentials.

## Example Calls

```csharp
var leadingPositions = await api.CopyTrading.GetLeadingPositionsAsync();
var leadingHistory = await api.CopyTrading.GetLeadingPositionsHistoryAsync();
var accountConfig = await api.CopyTrading.GetAccountConfigurationAsync();
var publicConfig = await api.CopyTrading.GetPublicConfigurationAsync();
var ranks = await api.CopyTrading.GetLeadTradersRanksAsync();
```

## Method Catalog

### Lead Trader Operations

- `GetLeadingPositionsAsync`
- `GetLeadingPositionsHistoryAsync`
- `PlaceLeadingStopOrderAsync`
- `CloseLeadingPositionAsync`
- `GetLeadingInstrumentsAsync`
- `SetLeadingInstrumentsAsync`
- `GetProfitSharingDetailsAsync`
- `GetTotalProfitSharingAsync`
- `GetUnrealizedProfitSharingDetailsAsync`
- `GetTotalUnrealizedProfitSharingAsync`
- `AmendProfitSharingRatioAsync`

### Copy Trader Configuration

- `GetAccountConfigurationAsync`
- `FirstCopySettingsAsync`
- `AmendCopySettingsAsync`
- `StopCopyingAsync`
- `GetCopySettingsAsync`
- `GetMyLeadTradersAsync`
- `GetCopyTradersAsync`

### Public Leaderboard and Analytics

- `GetPublicConfigurationAsync`
- `GetLeadTradersRanksAsync`
- `GetLeadTraderWeeklyPnlAsync`
- `GetLeadTraderDailyPnlAsync`
- `GetLeadTraderStatsAsync`
- `GetLeadTraderCurrencyPreferencesAsync`
- `GetLeadTraderCurrentPositionsAsync`
- `GetLeadTraderPositionHistoryAsync`

## Tips

- Copy trading has both account-specific and public discovery endpoints; keep those flows separate in your application.
- Profit-sharing and leader ranking endpoints are useful for dashboards and analytics, not just trading actions.


