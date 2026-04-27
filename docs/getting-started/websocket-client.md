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


