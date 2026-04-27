# REST Client Reference

[Docs Home](../index.md) | [Installation](../getting-started/installation.md) | [Creating REST Clients](../getting-started/creating-rest-clients.md) | [Changelog](../changelog/index.md)

This reference follows the structure of `OkxRestApiClient`.

## Client Model

```csharp
var api = new OkxRestApiClient();

// examples
var balances = await api.Account.GetBalancesAsync();
var ticker = await api.Public.GetTickerAsync("BTC-USDT");
var funding = await api.Funding.GetBalancesAsync();
```

## Notes

- The pages document functional methods once.
- If a method also has a typed request-model overload, that is mentioned on the page instead of being listed as a separate endpoint.
- Authentication requirements are called out per section.

## Section Pages

- [Account](./account.md) - trading account balances, positions, leverage, fees, margin, and portfolio configuration
- [Trade](./trade.md) - regular order placement, amendment, cancellation, history, easy convert, and one-click repay
- [Algo](./algo.md) - conditional, trigger, and other OKX algo orders
- [Grid](./grid.md) - grid trading creation, amendment, stopping, AI parameters, and margin workflows
- [DCA](./dca.md) - DCA order lifecycle, cycles, manual buy, and margin management
- [Signal Bot](./signal-bot.md) - signal channels, bot orders, positions, and sub-orders
- [Recurring Buy](./recurring-buy.md) - recurring buy order lifecycle and history
- [Copy Trading](./copy-trading.md) - lead trader and copy trader workflows
- [Public and Market Data](./public.md) - market data, public data, and event contracts
- [Block Trading](./block.md) - RFQs, quotes, MMP, trades, and public block trade data
- [Spread Trading](./spread.md) - private spread trading plus public spread market data
- [Trading Statistics (Rubik)](./trading-statistics.md) - open interest, ratios, taker flow, and option statistics
- [Funding Account](./funding.md) - balances, transfers, deposit/withdrawal, convert, fiat, and buy/sell
- [Sub Account](./sub-account.md) - sub-account creation, keys, balances, bills, and transfers
- [Financial Products](./financial.md) - on-chain earn, staking, simple earn, flexible loan, and dual investment
- [Broker](./broker.md) - broker client entry points and current implementation status
- [Affiliate](./affiliate.md) - invitee and rebate information


