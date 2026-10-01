# WebSocket Quick Start

[Docs Home](../index.md) | [Installation](./installation.md) | [Creating REST Clients](./creating-rest-clients.md) | [REST Reference](../reference/index.md)

## Create a WebSocket Client

```csharp
var ws = new OkxWebSocketApiClient();
```

Like the REST client, the WebSocket client can be created with options:

```csharp
var options = new OkxWebSocketApiOptions
{
    DemoTradingService = false
};

var ws = new OkxWebSocketApiClient(options);
```

## WebSocket Port Migration

Default production and demo URLs use `wss://` on port **443**, without an explicit port:

- Production: `wss://ws.okx.com/ws/v5/public`, `/ws/v5/private`, and `/ws/v5/business`.
- Demo: `wss://wspap.okx.com/ws/v5/public`, `/ws/v5/private`, and `/ws/v5/business`.

OKX's [September 30, 2026 migration announcement](https://www.okx.com/en-us/help/okx-websocket-port-8443-discontinuation-announcement) confirms that port 443 already works and that port **8443 stops accepting connections on October 31, 2026**. Only the port changes; hosts, paths, authentication, and REST endpoints remain unchanged. The latest announcement takes precedence over the older `:8443` examples still present in the main API guide at the time of this update.

If your application replaces the `OkxAddress.Default` or `OkxAddress.Demo` WebSocket addresses, remove `:8443` from those custom URLs too. The wrapper does not rewrite custom addresses or reconnect existing application sessions as part of this change.

## Set Credentials

Private subscriptions and trading operations require credentials:

```csharp
ws.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");
```

## Public Subscription Example

```csharp
var subscription = await ws.Public.SubscribeToTickersAsync(
    data => Console.WriteLine($"{data.InstrumentId} {data.LastPrice}"),
    "BTC-USDT");
```

## Private Subscription Example

```csharp
ws.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var accountSubscription = await ws.Account.SubscribeToAccountUpdatesAsync(
    data => Console.WriteLine($"Updated account snapshot with {data.Details.Count} balance entries"));
```

## Unsubscribe

`SubscribeTo...Async` methods return a subscription object that can be passed to `UnsubscribeAsync`:

```csharp
var subscription = await ws.Public.SubscribeToTradesAsync(
    data => Console.WriteLine(data.InstrumentId),
    "BTC-USDT");

await ws.UnsubscribeAsync(subscription.Data!);
```

## Service Upgrade Notices

OKX sends code `64008` roughly 60 seconds before a public, private, or business WebSocket connection is closed for a service upgrade. Register the handler before subscribing:

```csharp
ws.ServiceUpgradeNotice += notice =>
{
    Console.WriteLine($"OKX will close connection {notice.ConnectionId}: {notice.Message}");
    // Establish a replacement connection and restore application state as appropriate.
};
```

The client exposes the notice but does not force a reconnect. Reconnect and subscription/state restoration must remain coordinated by the application so an in-flight trading workflow is not changed implicitly.

## Section Overview

The WebSocket client currently exposes:

- `ws.Public`
- `ws.Account`
- `ws.Trade`
- `ws.Algo`
- `ws.Grid`
- `ws.RecurringBuy`
- `ws.CopyTrading`
- `ws.Funding`
- `ws.Block`
- `ws.Spread`

The REST reference in this documentation focuses on `OkxRestApiClient`, but the examples project also includes WebSocket sample usage:

- [OKX.Api.Examples/Program.cs](../../OKX.Api.Examples/Program.cs)

## Demo Trading

```csharp
var ws = new OkxWebSocketApiClient(new OkxWebSocketApiOptions
{
    DemoTradingService = true
});
```

The client will automatically route subscriptions and authenticated operations to the correct OKX public, private, or business WebSocket endpoint for the selected environment.


