# Creating REST Clients

[Docs Home](../index.md) | [Installation](./installation.md) | [Authentication](./authentication.md) | [REST Reference](../reference/index.md)

## Default Client

The simplest constructor is parameterless:

```csharp
var api = new OkxRestApiClient();
```

This is usually enough for public endpoints.

## Create with Options

Use `OkxRestApiOptions` when you want to control client behavior up front:

```csharp
var options = new OkxRestApiOptions
{
    AutoTimestamp = true,
    AutoTimestampInterval = TimeSpan.FromHours(1),
    SignPublicRequests = false,
    DemoTradingService = false,
};

var api = new OkxRestApiClient(options);
```

## Create with Credentials

You can pass credentials at construction time:

```csharp
var credentials = new OkxApiCredentials(
    "YOUR-API-KEY",
    "YOUR-API-SECRET",
    "YOUR-API-PASSPHRASE");

var options = new OkxRestApiOptions(credentials);
var api = new OkxRestApiClient(options);
```

Or you can set them later:

```csharp
var api = new OkxRestApiClient();
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");
```

## Demo Trading

To point the REST client at OKX demo endpoints:

```csharp
var options = new OkxRestApiOptions
{
    DemoTradingService = true
};

var api = new OkxRestApiClient(options);
```

Setting `DemoTradingService = true` switches the base address to the OKX demo REST API.

## Public Request Signing

Some OKX endpoints are technically in public sections but may support or require signed requests in specific scenarios. The client exposes:

```csharp
var options = new OkxRestApiOptions
{
    SignPublicRequests = true
};
```

In practice, most users should leave this at the default `false` and only opt in when they explicitly need signed public calls.

## Request-Model Overloads

Many filter-heavy methods now support two styles:

1. positional parameters for short calls
2. typed request models for long or evolving requests

For example:

```csharp
var orders1 = await api.Trade.GetOpenOrdersAsync(
    instrumentType: OkxInstrumentType.Swap,
    instrumentFamily: "BTC-USDT");

var orders2 = await api.Trade.GetOpenOrdersAsync(new OkxTradeOpenOrdersRequest
{
    InstrumentType = OkxInstrumentType.Swap,
    InstrumentFamily = "BTC-USDT"
});
```

Both styles are supported. The typed request form is usually easier to maintain when a method takes many optional filters.

## Result Pattern

Calls return `RestCallResult<T>`:

```csharp
var result = await api.Public.GetServerTimeAsync();

if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data);
```

## Related Pages

- [Authentication and Public vs Private Requests](./authentication.md)
- [WebSocket Quick Start](./websocket-client.md)
- [REST Client Reference](../reference/index.md)


