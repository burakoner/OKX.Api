# OKX.Api

`OKX.Api` is a .NET wrapper for the OKX V5 REST and WebSocket APIs.

The project aims to stay close to the official OKX surface while still feeling natural in C#:

- section-based clients such as `api.Account`, `api.Trade`, `api.Public`, and `api.Funding`
- typed request and response models instead of loose dictionaries
- REST and WebSocket coverage in the same package
- support for both public and authenticated workflows
- examples, fixtures, and integration coverage for the live API

## Installation

Available on [NuGet](https://www.nuget.org/packages/OKX.Api):

```powershell
Install-Package OKX.Api
```

The package currently targets:

- `netstandard2.0`
- `netstandard2.1`

## Quick Start

Add the namespace:

```csharp
using OKX.Api;
```

Create a REST client for public endpoints:

```csharp
var api = new OkxRestApiClient();

var instruments = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Spot);
var ticker = await api.Public.GetTickerAsync("BTC-USDT");

if (ticker.Success)
{
    Console.WriteLine($"{ticker.Data!.InstrumentId}: {ticker.Data.LastPrice}");
}
```

Create a REST client for private endpoints:

```csharp
var api = new OkxRestApiClient();
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var balances = await api.Account.GetBalancesAsync();
var openOrders = await api.Trade.GetOpenOrdersAsync();
```

Create a WebSocket client:

```csharp
var ws = new OkxWebSocketApiClient();

var subscription = await ws.Public.SubscribeToTickersAsync(
    data => Console.WriteLine($"{data.InstrumentId} {data.LastPrice}"),
    "BTC-USDT");

// later
await ws.UnsubscribeAsync(subscription.Data!);
```

## Documentation

The root `README` is intentionally short. Full documentation now lives in [`docs/`](./docs/index.md).

- [Documentation Home](./docs/index.md)
- [Installation](./docs/getting-started/installation.md)
- [Creating REST Clients](./docs/getting-started/creating-rest-clients.md)
- [Authentication and Public vs Private Requests](./docs/getting-started/authentication.md)
- [WebSocket Quick Start](./docs/getting-started/websocket-client.md)
- [REST Client Reference](./docs/reference/index.md)
- [Changelog](./docs/changelog/index.md)

## REST Sections

`OkxRestApiClient` is organized around OKX sections:

- `Account`
- `Trade`
- `Algo`
- `Grid`
- `DCA`
- `SignalBot`
- `RecurringBuy`
- `CopyTrading`
- `Public` (`Market` is an alias)
- `Block`
- `Spread`
- `Rubik` (`TradingStatistics` is an alias)
- `Funding`
- `SubAccount`
- `Financial`
- `Broker`
- `Affiliate`

Each section now has its own reference page under [`docs/reference`](./docs/reference/index.md).

## Examples

The repository includes a dedicated examples project:

- [OKX.Api.Examples/Program.cs](./OKX.Api.Examples/Program.cs)

It contains end-to-end sample calls across REST and WebSocket sections and is a good place to explore the current API surface quickly.

## Changelog

Release notes were moved out of the root file into split documents:

- [Changelog Index](./docs/changelog/index.md)
- [2026 Releases](./docs/changelog/2026.md)
- [2023-2025 Archive](./docs/changelog/archive-2023-2025.md)

## Official OKX Documentation

This wrapper follows the official OKX documentation:

- [OKX API Docs](https://www.okx.com/docs-v5/en/)

Where behavior differs in practice, the wrapper and its tests prefer live API behavior over stale examples whenever possible.

## Support

If you think something is broken, something is missing, or you have a question, please open an [issue](https://github.com/burakoner/OKX.Api/issues).

If you want to support maintenance, the original donation addresses are kept here:

- `BTC`: `33WbRKqt7wXARVdAJSu1G1x3QnbyPtZ2bH`
- `ETH`: `0x65b02db9b67b73f5f1e983ae10796f91ded57b64`
- `USDT (TRC-20)`: `TXwqoD7doMESgitfWa8B2gHL7HuweMmNBJ`
