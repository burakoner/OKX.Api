# Authentication and Public vs Private Requests

[Docs Home](../index.md) | [Installation](./installation.md) | [Creating REST Clients](./creating-rest-clients.md) | [REST Reference](../reference/index.md)

## Credentials

Private OKX endpoints require:

- API key
- API secret
- API passphrase

The library models them with `OkxApiCredentials`:

```csharp
var credentials = new OkxApiCredentials(
    "YOUR-API-KEY",
    "YOUR-API-SECRET",
    "YOUR-API-PASSPHRASE");
```

## Set Credentials on a REST Client

```csharp
var api = new OkxRestApiClient();
api.SetApiCredentials(credentials);
```

Or:

```csharp
var api = new OkxRestApiClient();
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");
```

## Public Requests

Public requests do not need credentials:

```csharp
var api = new OkxRestApiClient();

var instruments = await api.Public.GetInstrumentsAsync(OkxInstrumentType.Spot);
var serverTime = await api.Public.GetServerTimeAsync();
var statistics = await api.Rubik.GetSupportCoinAsync();
```

Typical public sections:

- `api.Public`
- many `api.Rubik` methods
- the public read-only surface inside `api.Spread` and `api.Block`

## Private Requests

Private requests require credentials:

```csharp
var api = new OkxRestApiClient();
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var balances = await api.Account.GetBalancesAsync();
var placeOrder = await api.Trade.PlaceOrderAsync(
    "BTC-USDT",
    OkxTradeMode.Cash,
    OkxTradeOrderSide.Buy,
    OkxTradePositionSide.Net,
    OkxTradeOrderType.MarketOrder,
    0.01m);
```

Typical private sections:

- `api.Account`
- `api.Trade`
- `api.Algo`
- `api.Grid`
- `api.DCA`
- `api.SignalBot`
- `api.RecurringBuy`
- `api.CopyTrading`
- `api.Funding`
- `api.SubAccount`
- most of `api.Financial`

## Mixed Sections

Some section clients contain both public and private methods.

Examples:

- `api.Public` contains public market data plus a few calls that can also be signed
- `api.Block` contains private RFQ/quote management and public block trade data
- `api.Spread` contains both private trading methods and public market data

When in doubt, check the section reference page and the matching official OKX docs link on that page.

## Demo Trading

Demo trading is controlled through the options object:

```csharp
var options = new OkxRestApiOptions
{
    DemoTradingService = true
};

var api = new OkxRestApiClient(options);
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");
```

## Security Notes

- never commit real API keys, secrets, or passphrases
- keep live and demo credentials separate
- treat any order-placement sample as a real trading action unless you are explicitly using demo endpoints

## Related Pages

- [Creating REST Clients](./creating-rest-clients.md)
- [WebSocket Quick Start](./websocket-client.md)
- [REST Client Reference](../reference/index.md)


