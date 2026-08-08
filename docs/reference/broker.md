# Broker

[Docs Home](../index.md) | [REST Reference](./index.md) | [Affiliate](./affiliate.md)

Official OKX docs: [OKX Broker API](https://www.okx.com/docs-v5/broker_en/)

## Overview

`api.Broker` is a container for broker-specific clients:

- `api.Broker.FD` for fully disclosed broker endpoints
- `api.Broker.DMA` for DMA broker endpoints

## Current Implementation Status

The FD client currently implements:

- `GetDownloadLinksAsync` for `GET /api/v5/broker/fd/rebate-per-orders`
- `GetRebateInformationAsync` for `GET /api/v5/broker/fd/if-rebate`

The remaining FD operation and all DMA operations are still marked as TODO. The broker section is therefore not feature-complete.

## Example Calls

```csharp
api.SetApiCredentials("YOUR-API-KEY", "YOUR-API-SECRET", "YOUR-API-PASSPHRASE");

var links = await api.Broker.FD.GetDownloadLinksAsync(
    allHistory: false,
    begin: new DateTime(2026, 5, 1),
    end: new DateTime(2026, 5, 14),
    brokerType: OkxBrokerType.Api);

var rebate = await api.Broker.FD.GetRebateInformationAsync(
    "USER-API-KEY",
    OkxBrokerType.Api);
```

`GetDownloadLinksAsync` returns links that OKX refreshes on each request and keeps valid for two hours. When `allHistory` is `false`, both dates are required; `begin` is inclusive and `end` is exclusive.

## FD Rebate CSV Columns

The downloaded FD rebate file currently contains:

- `brokerCode`, `level`, `uid`, `instId`, `ordId`, `clOrdId`
- `spotTradeAmt`, `derivativeTradeAmt`, `fee`, `netFee`, `settlementFee`
- `brokerRebate`, `suBrokerRebate`, `userRebate`, `affiliated`, `ts`

`clOrdId` is an empty string when the original order did not include a client order ID.

## Rebate Eligibility

`OkxFDBrokerRebateInformation.Status` reports why a broker rebate is unavailable. `Eligible` means the broker can receive a rebate; `MsaNotEligible` represents OKX response `type=4`.


