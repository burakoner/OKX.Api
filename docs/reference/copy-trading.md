# Copy Trading

[Docs Home](../index.md) | [REST Reference](./index.md) | [Signal Bot](./signal-bot.md) | [Affiliate](./affiliate.md)

Official OKX docs: [Order Book Trading](https://www.okx.com/docs-v5/en/#order-book-trading)

## Overview

`api.CopyTrading` contains both lead-trader and copy-trader workflows. Account-specific configuration and trading methods require credentials; leaderboard, configuration, and lead-trader analytics methods whose route begins with `public-` are unsigned public requests.

## Example Calls

```csharp
var leadingPositions = await api.CopyTrading.GetLeadingPositionsAsync();
var leadingHistory = await api.CopyTrading.GetLeadingPositionsHistoryAsync();
var accountConfig = await api.CopyTrading.GetAccountConfigurationAsync();
var publicConfig = await api.CopyTrading.GetPublicConfigurationAsync();
var ranks = await api.CopyTrading.GetLeadTradersRanksAsync();
var stats = await api.CopyTrading.GetLeadTraderStatsAsync(
    "213E8C92DC61EFAC",
    OkxCopyTradingPerformancePeriod.Last30Days);
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

### Public Leaderboard and Analytics

- `GetPublicConfigurationAsync`
- `GetLeadTradersRanksAsync`
- `GetLeadTraderWeeklyPnlAsync`
- `GetLeadTraderDailyPnlAsync`
- `GetLeadTraderStatsAsync`
- `GetLeadTraderCurrencyPreferencesAsync`
- `GetLeadTraderCurrentPositionsAsync`
- `GetLeadTraderPositionHistoryAsync`
- `GetCopyTradersAsync`

## Tips

- Copy trading has both account-specific and public discovery endpoints; keep those flows separate in your application.
- Lead trader `uniqueCode` values must contain exactly 16 or 18 case-sensitive alphanumeric characters.
- Daily PnL and statistics use the typed `OkxCopyTradingPerformancePeriod` values for the documented 7, 30, 90, and 365-day windows. The string overloads remain available for compatibility and validate the same values.
- The current official documentation exposes the analytics methods in this catalog as public endpoints. Historical private analytics and multiple-leverage changelog links no longer identify active documented endpoints, so they are not exposed as speculative wrappers.
- Profit-sharing and leader ranking endpoints are useful for dashboards and analytics, not just trading actions.


