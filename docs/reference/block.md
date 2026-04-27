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

## Tips

- Keep public and private block flows separate in your application design; they serve very different use cases.
- For request-heavy RFQ and quote creation, prefer typed request models where available.


