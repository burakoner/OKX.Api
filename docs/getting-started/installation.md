# Installation

[Docs Home](../index.md) | [Creating REST Clients](./creating-rest-clients.md) | [Authentication](./authentication.md) | [REST Reference](../reference/index.md)

## Package

Install `OKX.Api` from NuGet:

```powershell
Install-Package OKX.Api
```

The package currently targets:

- `netstandard2.0`
- `netstandard2.1`

## Namespace

Add the root namespace:

```csharp
using OKX.Api;
```

Most enums and request/response models also live under the root `OKX.Api` namespace, so many examples only need that single import.

## What Gets Installed

The package gives you two top-level entry points:

- `OkxRestApiClient` for REST endpoints
- `OkxWebSocketApiClient` for WebSocket subscriptions and operations

The REST client is further split into section clients such as:

- `api.Account`
- `api.Trade`
- `api.Public`
- `api.Funding`
- `api.SubAccount`
- `api.Financial`

## First Public Request

No API key is required for public endpoints:

```csharp
using OKX.Api;

var api = new OkxRestApiClient();
var ticker = await api.Public.GetTickerAsync("BTC-USDT");

if (ticker.Success)
{
    Console.WriteLine(ticker.Data!.LastPrice);
}
```

## First Private Request

Private endpoints require an API key, secret, and passphrase:

```csharp
using OKX.Api;

var api = new OkxRestApiClient();
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var balances = await api.Account.GetBalancesAsync();
```

## Recommended Next Reads

- [Creating REST Clients](./creating-rest-clients.md)
- [Authentication and Public vs Private Requests](./authentication.md)
- [WebSocket Quick Start](./websocket-client.md)
- [REST Client Reference](../reference/index.md)


