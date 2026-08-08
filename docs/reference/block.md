# Block Trading

[Docs Home](../index.md) | [REST Reference](./index.md) | [Spread](./spread.md) | [Trade](./trade.md)

Official OKX docs: [Block Trading](https://www.okx.com/docs-v5/en/#block-trading)

## Overview

`api.Block` combines private RFQ and quote management with public block-trade market data.

It is a mixed section:

- RFQs, quotes, MMP, and private trade queries require credentials
- public block tickers and public block trade feeds do not

## Example Calls

```csharp
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var counterparties = await api.Block.GetCounterpartiesAsync();
var quoteProducts = await api.Block.GetQuoteProductsAsync();
var mmp = await api.Block.GetMmpAsync();
var rfqs = await api.Block.GetRfqsAsync();
var quotes = await api.Block.GetQuotesAsync();
```

Public examples:

```csharp
var tickers = await api.Block.GetTickersAsync(OkxInstrumentType.Option, instrumentFamily: "BTC-USD");
var ticker = await api.Block.GetTickerAsync("BTC-USD-240628-50000-C");
var publicTrades = await api.Block.GetPublicExecutedTradesAsync();
```

## Method Catalog

### RFQs and Quotes

- `GetCounterpartiesAsync`
- `CreateRfqAsync`
- `CancelRfqAsync`
- `CancelRfqsAsync`
- `CancelAllRfqsAsync`
- `ExecuteQuoteAsync`
- `GetQuoteProductsAsync`
- `SetQuoteProductsAsync`
- `CreateQuoteAsync`
- `CancelQuoteAsync`
- `CancelQuotesAsync`
- `CancelAllQuotesAsync`
- `CancelAllQuotesAfterAsync`

### MMP and Private Queries

- `ResetMmpAsync`
- `SetMmpAsync`
- `GetMmpAsync`
- `GetRfqsAsync`
- `GetQuotesAsync`
- `GetTradesAsync`

### Public Market Data

- `GetTickersAsync`
- `GetTickerAsync`
- `GetPublicExecutedTradesAsync`
- `GetPublicRecentTradesAsync`

### WebSocket Channels

- `SubscribeToRfqsUpdatesAsync`
- `SubscribeToUserStructureTradesAsync`
- `SubscribeToPublicStructureTradesAsync`

## Tips

- Keep public and private block flows separate in your application design; they serve very different use cases.
- For request-heavy RFQ and quote creation, prefer typed request models where available.

## RFQ State and Trade Mapping Semantics

- `filled` means the RFQ was executed against that maker's quote.
- `traded_away` is maker-only. A taker can execute Maker A's quote, so Maker A sees `filled` while another invited Maker B sees the same RFQ as `traded_away`.
- The private `struc-block-trades` channel pushes only to the taker and the executing maker. A maker that sees `traded_away` does not receive that trade update.
- In the public `public-struc-block-trades` channel, a normal RFQ has a one-to-one `blockTdId` to `rfqId` relationship. A Group RFQ can map one `rfqId` to multiple `blockTdId` values; counterparties can cross-reference both identifiers through the private structure channel.
- Parent-level Group RFQ structure updates can return empty `blockTdId` and leg `tradeId` values. The wrapper exposes those fields as nullable values and keeps the per-account identifiers in `AccountLevelAllocations`.


