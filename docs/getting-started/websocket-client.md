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

## Instrument Catalog Updates

For `SubscribeToInstrumentsAsync`, do not expect an initial full list or replace the whole catalog on each push. Some scenarios now send only changed instruments; update entries by `InstrumentId` and reconcile missing/removal cases deliberately through REST. The wrapper forwards individual records and maintains no catalog. See the [incremental catalog contract](../reference/public.md#instrument-catalog-and-incremental-updates).

Affected Crypto-USD SPOT pairs now use new Crypto-USDC IDs **and codes**, without server-side legacy mapping. Refresh subscriptions and all ID/code caches yourself; WS placement needs the new integer code. If keeping USD trading, explicitly select `TradeQuoteCurrency = "USD"` when the account's quote list permits it. Omission on a new USDC pair defaults to USDC, subject to regional/account rules. See the [migration contract](../reference/trade.md#crypto-usd-to-crypto-usdc-migration); activation and order replay are never automatic.

## Private Subscription Example

```csharp
ws.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var accountSubscription = await ws.Account.SubscribeToAccountUpdatesAsync(
    data => Console.WriteLine($"Updated account snapshot with {data.Details.Count} balance entries"));
```

## Order Updates and Connection Limits

```csharp
ws.ChannelConnectionCount += notice =>
{
    if (notice.IsLimitError)
    {
        Console.WriteLine($"OKX terminated {notice.Channel} on connection {notice.ConnectionId}");
        // Coordinate subscription restoration and state reconciliation in the application.
    }
};

var orderSubscription = await ws.Trade.SubscribeToOrderUpdatesAsync(
    order => Console.WriteLine($"{order.OrderId}: {order.OrderState}, cancel source {order.CancelSource}"),
    OkxInstrumentType.Any);
```

The [orders channel](https://www.okx.com/docs-v5/en/#order-book-trading-trade-ws-order-channel) sends no initial snapshot. Subscribe before placing orders and reconcile existing/pending state through REST deliberately. Placement acknowledgement is not proof of book entry: post-only/MMP post-only/RPI orders can first report only `Canceled`, without `Live`. System size/price adjustments can produce two consecutive `Live` updates, with amendment metadata in the first. The wrapper forwards both and leaves deduplication/reconciliation to the application; see the [trade reference](../reference/trade.md#orders-channel-placement-state-and-reconciliation).

The follow-up after the 5.6.820 review checkpoint, included in the 5.6.826 source/package build, adds request-scoped subscribe IDs and waits for all requested argument ACKs before success. Generic subscription errors cannot complete trading queries. Missing/foreign identities and incomplete confirmation remain failures with an uncertain outcome; do not blindly retry orders or assume all server-side subscriptions were rolled back. See the [correlation/confirmation contract](../reference/trade.md#websocket-response-correlation-and-confirmation). Previously built/installed 5.6.820 packages do not establish that these fixes are present; no package publication or application deployment is implied.

Login failures return their documented numeric codes/raw responses, including `event:error / 60009`. Authentication-specific errors are handled only in the authentication wait; unidentified generic request/subscription errors are not inferred to be login failures from their message text.

The [approved safety follow-up](../maintenance-plan.md#websocket-safety-follow-up--2026-10-01) closes expired/terminal login callbacks, so late replies cannot change their completed outcome or the wrapper's public `IsAuthendicated` flag. That legacy flag is still client-level, not a per-connection readiness/disconnect signal. No SDK authentication/lifecycle redesign, login retry, or trading replay is added. Rebuild from the updated source; previously built/installed 5.6.930 packages do not establish that this unreleased follow-up is present.

OKX's [channel connection limit](https://www.okx.com/docs-v5/en/#overview-websocket-connection-count-limit) is 30 connections per affected channel per sub-account. `ChannelConnectionCount` reports both counts and limit errors. A limit error can arrive **after** successful subscription acknowledgement and terminate that channel subscription. This event is notification only: it does not automatically reconnect, restore subscriptions, update the returned subscription object's lifecycle, or retry orders.

## Unsubscribe

`SubscribeTo...Async` methods return a subscription object. Use the explicit confirmation API when you need to distinguish local closure from server acknowledgement:

```csharp
var subscription = await ws.Public.SubscribeToTradesAsync(
    data => Console.WriteLine(data.InstrumentId),
    "BTC-USDT");

if (!subscription.Success)
    throw new InvalidOperationException(subscription.Error?.ToString());

var closed = await ws.UnsubscribeWithConfirmationAsync(subscription.Data!);
Console.WriteLine($"Local closed: {closed.Data.LocalClosed}; server confirmed: {closed.Data.ServerConfirmed}");
if (!closed.Success)
{
    Console.WriteLine(closed.Error);
    // Reconcile uncertain remote state deliberately; this is not permission to replay orders.
}
```

The new call retains SDK local closure even on incomplete/rejected ACKs, but returns `Success=false`, `Data.ServerConfirmed=false`, and the available numeric error/raw reply. `LocalClosed` requires the SDK closed flag plus removal from the connection's subscription registry; setting the flag alone is not completion. It is not proof of remote removal or completion of callbacks already being dispatched. `ServerConfirmed` requires ACKs for every distinct requested argument; false does not mean all remote filters remain active. A skipped server wait is not success. Foreign/internal/already-marked-closed handles and a second concurrent new call on the same handle do not trigger another close/send. Different subscriptions have isolated results. Unexpected SDK/transport exceptions propagate; do not mix concurrent legacy close/reconnect calls on the same handle. See the [complete contract](../reference/trade.md#websocket-response-correlation-and-confirmation).

Existing `await ws.UnsubscribeAsync(subscription.Data!)` remains available as a local-close operation. The ID-based overload can still return true despite incomplete/rejected server confirmation; the object overload exposes no confirmation result. Do not interpret either as proof of remote removal, especially on a shared open connection. No extra unsubscribe, automatic retry/restoration/reconciliation, or order replay is added by the new API.

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


