# OKX.Api Documentation

[README](../README.md) | [Installation](./getting-started/installation.md) | [REST Reference](./reference/index.md) | [Changelog](./changelog/index.md)

This documentation set is organized around the actual client structure exposed by `OkxRestApiClient` and `OkxWebSocketApiClient`.

## Start Here

- [Installation](./getting-started/installation.md)
- [Creating REST Clients](./getting-started/creating-rest-clients.md)
- [Authentication and Public vs Private Requests](./getting-started/authentication.md)
- [WebSocket Quick Start](./getting-started/websocket-client.md)

## REST Client Reference

The REST client is split into section clients that mirror the official OKX documentation layout:

- [Account](./reference/account.md)
- [Trade](./reference/trade.md)
- [Algo](./reference/algo.md)
- [Grid](./reference/grid.md)
- [DCA](./reference/dca.md)
- [Signal Bot](./reference/signal-bot.md)
- [Recurring Buy](./reference/recurring-buy.md)
- [Copy Trading](./reference/copy-trading.md)
- [Public and Market Data](./reference/public.md)
- [Block Trading](./reference/block.md)
- [Spread Trading](./reference/spread.md)
- [Trading Statistics (Rubik)](./reference/trading-statistics.md)
- [Funding Account](./reference/funding.md)
- [Sub Account](./reference/sub-account.md)
- [Financial Products](./reference/financial.md)
- [Broker](./reference/broker.md)
- [Affiliate](./reference/affiliate.md)

## Reading the Reference

Each section page follows the same structure:

- an overview of the client and the matching OKX area
- whether the section is public, private, or mixed
- a few realistic code examples
- a method catalog that lists the current supported surface

To keep the pages readable, method catalogs document each functional endpoint once. Overloads that only swap positional parameters for typed request models are described together.

## Examples Project

For a broad, compile-friendly sample of the current surface, see:

- [OKX.Api.Examples/Program.cs](../OKX.Api.Examples/Program.cs)

## Changelog

Release notes were split into separate pages:

- [Changelog Home](./changelog/index.md)
- [2026 Releases](./changelog/2026.md)
- [2023-2025 Archive](./changelog/archive-2023-2025.md)


